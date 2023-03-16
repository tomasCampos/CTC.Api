using CTC.Application.Features.User.UseCases.RegisterUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using FirebaseAdmin.Auth;
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
        private readonly string AESKey;

        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";

        public RegisterUserUseCase(
            IRequestValidator<RegisterUserInput> validator, 
            IRegisterUserRepository repository, 
            IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;

            AESKey = Environment.GetEnvironmentVariable(CypherAesKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {CypherAesKeyEnvironmentVariableName}");
        }

        public async Task<Output> Execute(RegisterUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterUserUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var userAlreadyExists = await _repository.VerifyIfUserAlreadyExists(input.UserEmail!, input.UserPhone!, input.UserDocument!) > 0;
            if (userAlreadyExists)
                return Output.CreateConflictResult("Já existe um usuário cadastrado com o email, telefone ou documento informados");

            var encryptedPassword = AES.Encrypt(input.UserPassword!, AESKey);
            var user = new UserModel(input.UserFirstName!, input.UserEmail!, input.UserPhone!, input.UserDocument!, input.UserLastName!, (int)input.UserPermission!, encryptedPassword);
            var wasUserInsertedInDataBaseWithSuccess = await _repository.InsertUser(user);
            if (!wasUserInsertedInDataBaseWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível cadastrar o usuário. Tente novamente mais tarde.");

            await RegisterFireBaseUser(input);

            return Output.CreateCreatedResult();
        }

        private async Task RegisterFireBaseUser(RegisterUserInput user)
        {
            var args = new UserRecordArgs()
            {
                Email = user.UserEmail,
                Password = user.UserPassword,
                DisplayName = $"{user.UserFirstName} {user.UserLastName} - {(int)user.UserPermission!}"
            };

            _ = await FirebaseAuth.DefaultInstance.CreateUserAsync(args);
        }
    }
}