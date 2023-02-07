using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase.IO;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase;
using CTC.Application.Features.User.UseCases.RegisterUser.Data;
using CTC.Application.Features.User.UseCases.RegisterUser.UseCase.IO;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.RegisterUser.UseCase
{
    internal sealed class RegisterUserUseCase : IUseCase<RegisterUserInput, RegisterUserOutput>
    {
        private readonly IRequestValidator<RegisterUserInput> _validator;
        private readonly IRegisterUserRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly string FireBaseApiKey;
        private readonly string AESKey;

        public RegisterUserUseCase(IRequestValidator<RegisterUserInput> validator, IRegisterUserRepository repository, IConfiguration configuration, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            FireBaseApiKey = configuration["FireBaseApiKey"]!;
            AESKey = configuration["CypherAESKey"]!;
        }

        public async Task<RegisterUserOutput> Execute(RegisterUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterUserUseCase), input.RequestUserPermission);
            if (!isAuthorized)
            {
                return new RegisterUserOutput
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    ValidationErrorMessage = "Falta de permissão para realizar tal ação"
                };
            }

            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                return new RegisterUserOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ValidationErrorMessage = validationResult.ErrorMessage
                };
            }

            var userAlreadyExists = await _repository.CountUserByEmail(input.UserEmail!) > 0;
            if (userAlreadyExists)
            {
                return new RegisterUserOutput
                {
                    StatusCode = HttpStatusCode.Conflict,
                    ValidationErrorMessage = $"Já existe um usuário cadastrado com este email: {input.UserEmail}"
                };
            }

            var encryptedPassword = AES.Encrypt(input.UserPassword!, AESKey);
            var user = new UserModel(input.UserFirstName!, input.UserEmail!, input.UserPhone!, input.UserDocument!, input.UserLastName!, (int)input.UserPermission!, encryptedPassword);
            var wasUserInsertedInDataBaseWithSuccess = await _repository.InsertUser(user);
            if (!wasUserInsertedInDataBaseWithSuccess)
            {
                return new RegisterUserOutput
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    ValidationErrorMessage = "Não foi possível cadastrar o usuário. Tente novamente mais tarde."
                };
            }

            var firebaseAuthLink = await RegisterFireBaseUser(input);

            return new RegisterUserOutput
            {
                StatusCode = HttpStatusCode.Created,
                Body = new { AuthToken = firebaseAuthLink }
            };
        }

        private async Task<string> RegisterFireBaseUser(RegisterUserInput user)
        {
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBaseApiKey));

            FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(user.UserEmail, user.UserPassword);
            return firebaseAuthLink.FirebaseToken;
        }
    }
}