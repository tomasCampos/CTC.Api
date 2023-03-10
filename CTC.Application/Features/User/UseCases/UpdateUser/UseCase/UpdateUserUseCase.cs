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

        private const string UseCaseFailMessage = "Falha ao atualizar o usuário, contate o administrador.";

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

            var result = await UpdateSqlServeUser(userToUpdate, input);

            if (!result.success)
                return Output.CreateInternalErrorResult(UseCaseFailMessage);

            if(result.needToUpdateFireBaseUser)
            {
                var success = await UpdateFireBaseUser(userToUpdate.Email!, input, userToUpdate);

                if (!success)
                    Output.CreateInternalErrorResult(UseCaseFailMessage);
            }

            return Output.CreateOkResult();
        }

        private async Task<(bool success, bool needToUpdateFireBaseUser)> UpdateSqlServeUser(UserModel currentUserData, UpdateUserInput newUserData)
        {
            var encryptedPassword = AES.Encrypt(newUserData.UserPassword!, AESKey);

            string? newUserDocument = newUserData.UserDocument == currentUserData.Document ? string.Empty : newUserData.UserDocument;
            string? newUserPhone = newUserData.UserPhone == currentUserData.Phone ? string.Empty : newUserData.UserPhone;
            string? newUserEmail = newUserData.UserEmail == currentUserData.Email ? string.Empty : newUserData.UserEmail;

            UserModel newUser;
            if (_userContext.UserPermission != UserPermission.Administrator)
                newUser = new UserModel(currentUserData.UserId!, currentUserData.PersonId!, newUserData.UserFirstName!, newUserEmail!, newUserPhone!, newUserData.UserLastName!, encryptedPassword, (int)currentUserData.Permission!, newUserDocument!);
            else
                newUser = new UserModel(currentUserData.UserId!, currentUserData.PersonId!, newUserData.UserFirstName!, newUserEmail!, newUserPhone!, newUserData.UserLastName!, encryptedPassword, (int)newUserData.UserPermission!, newUserDocument!);

            var result = await _repository.UpdateUser(newUser);

            var wasPasswordOrEmailUpdated = ((encryptedPassword != currentUserData.Password) || !string.IsNullOrEmpty(newUserEmail));
            return (result.success, wasPasswordOrEmailUpdated);
        }

        private async Task<bool> UpdateFireBaseUser(string currentUserEmail, UpdateUserInput newUser, UserModel oldUser)
        {
            try
            {
                var userRecord = await FirebaseAuth.DefaultInstance.GetUserByEmailAsync(currentUserEmail);
                var args = new UserRecordArgs()
                {
                    Uid = userRecord.Uid,
                    Email = newUser.UserEmail,
                    Password = newUser.UserPassword
                };

                _ = await FirebaseAuth.DefaultInstance.UpdateUserAsync(args);
                return true;
            }
            catch
            {
                _ = await _repository.UpdateUser(oldUser);
                return false;
            }
        }
    }
}
