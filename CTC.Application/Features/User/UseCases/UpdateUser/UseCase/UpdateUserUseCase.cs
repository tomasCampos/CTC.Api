using CTC.Application.Features.User.UseCases.UpdateUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UserContext;
using FirebaseAdmin.Auth;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.UseCase
{
    internal sealed class UpdateUserUseCase : IUseCase<UpdateUserInput, Output>
    {
        private readonly IRequestValidator<UpdateUserInput> _validator;
        private readonly IUpdateUserRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IUserContext _userContext;
        private readonly string FireBaseApiKey;
        private readonly string AESKey;

        private const string FireBaseApiKeyEnvironmentVariableName = "FIRE_BASE_API_KEY";
        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";

        public UpdateUserUseCase(IRequestValidator<UpdateUserInput> validator, IUpdateUserRepository repository, IUseCaseAuthorizationService useCaseAuthorizationService, IUserContext userContext)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _userContext = userContext;

            FireBaseApiKey = Environment.GetEnvironmentVariable(FireBaseApiKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {FireBaseApiKeyEnvironmentVariableName}");

            AESKey = Environment.GetEnvironmentVariable(CypherAesKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {CypherAesKeyEnvironmentVariableName}");

        }

        public async Task<Output> Execute(UpdateUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateUserUseCase), input.UserEmail);
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var userToUpdate = await _repository.GetUserById(input.UserId!);

            if(userToUpdate == null)
                return Output.CreateInvalidParametersResult("O usuário a ser atualizado não existe");

            var encryptedPassword = AES.Encrypt(input.UserPassword!, AESKey);

            UserModel newUser;
            if (_userContext.UserPermission != UserPermission.Administrator)
                newUser = new UserModel(input.UserId!, userToUpdate.PersonId!, input.UserFirstName!, input.UserEmail!, input.UserPhone!, input.UserDocument!, input.UserLastName!, (int)userToUpdate.Permission!, encryptedPassword);
            else
                newUser = new UserModel(input.UserId!, userToUpdate.PersonId!, input.UserFirstName!, input.UserEmail!, input.UserPhone!, input.UserDocument!, input.UserLastName!, (int)input.UserPermission!, encryptedPassword);

            var result = await _repository.UpdateUser(newUser);

            //TODO: Remover unique de telefone, documento e email do banco de dados, ta dando erro ao atualizar esses campos, quando são iguais aos que já estão no banco pro usuário atualizado.
            if (!result.success)
                return Output.CreateInternalErrorResult("Falha ao atualizar o usuário. Verifique com o admministrador se o número de telefone, email e/ou número de documento informados já estão cadastrados para outro usuário.");

            await UpdateFireBaseUser(userToUpdate, input);

            return Output.CreateOkResult();
        }

        private async Task UpdateFireBaseUser(UserModel currentUser, UpdateUserInput newUser)
        {
            var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(currentUser.Email);
            var args = new UserRecordArgs()
            {
                Uid = userRecord.Uid,
                Email = newUser.UserEmail,
                Password = newUser.UserPassword
            };

            _ = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);
        }
    }
}
