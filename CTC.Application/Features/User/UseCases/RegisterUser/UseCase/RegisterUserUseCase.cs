using CTC.Application.Features.User.UseCases.RegisterUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UserContext;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.RegisterUser.UseCase
{
    internal sealed class RegisterUserUseCase : IUseCase<RegisterUserInput, Output>
    {
        private readonly IRequestValidator<RegisterUserInput> _validator;
        private readonly IRegisterUserRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly string FireBaseApiKey;
        private readonly string AESKey;

        private const string FireBaseApiKeyEnvironmentVariableName = "FIRE_BASE_API_KEY";
        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";

        public RegisterUserUseCase(
            IRequestValidator<RegisterUserInput> validator, 
            IRegisterUserRepository repository, 
            IConfiguration configuration, 
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IUserContext userContext)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;

            FireBaseApiKey = Environment.GetEnvironmentVariable(FireBaseApiKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {FireBaseApiKeyEnvironmentVariableName}");

            AESKey = Environment.GetEnvironmentVariable(CypherAesKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {CypherAesKeyEnvironmentVariableName}");
        }

        public async Task<Output> Execute(RegisterUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterUserUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var userAlreadyExists = await _repository.VerifyIfUserAlreadyExists(input.UserEmail!, input.UserPhone!, input.UserDocument!) > 0;
            if (userAlreadyExists)
                return Output.CreateConflictResult("Já existe um usuário cadastrado com o email, telefone ou documento informados");

            var encryptedPassword = AES.Encrypt(input.UserPassword!, AESKey);
            var user = new UserModel(input.UserFirstName!, input.UserEmail!, input.UserPhone!, input.UserDocument!, input.UserLastName!, (int)input.UserPermission!, encryptedPassword);
            var wasUserInsertedInDataBaseWithSuccess = await _repository.InsertUser(user);
            if (!wasUserInsertedInDataBaseWithSuccess)
                return Output.CreateInternalErrorResult("Não foi possível cadastrar o usuário. Tente novamente mais tarde.");

            var firebaseAuthLink = await RegisterFireBaseUser(input);

            return Output.CreateCreatedResult(new { AuthToken = firebaseAuthLink });
        }

        private async Task<string> RegisterFireBaseUser(RegisterUserInput user)
        {
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBaseApiKey));

            FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.CreateUserWithEmailAndPasswordAsync(user.UserEmail, user.UserPassword);
            return firebaseAuthLink.FirebaseToken;
        }
    }
}