using CTC.Application.Features.User.Services.Firebase;
using CTC.Application.Features.User.UseCases.RegisterUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
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
        private readonly IFirebaseService _firebaseService;
        private readonly string AESKey;

        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";

        public RegisterUserUseCase(
            IRequestValidator<RegisterUserInput> validator, 
            IRegisterUserRepository repository, 
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IFirebaseService firebaseService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _firebaseService = firebaseService;

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

            await _firebaseService.RegisterFireBaseUser(input.UserPassword!, input.UserEmail!, $"{input.UserFirstName} {input.UserLastName} - {(int)input.UserPermission!}");
            return Output.CreateCreatedResult();
        }
    }
}