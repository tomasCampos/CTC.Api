using CTC.Application.Features.User.UseCases.UpdateUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UserContext;
using CTC.Application.Shared.UserContext.Services;
using FirebaseAdmin.Auth;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.UseCase
{
    internal sealed class UpdateUserUseCase : IUseCase<UpdateUserInput, Output>
    {
        private readonly IRequestValidator<UpdateUserInput> _validator;
        private readonly IUpdateUserRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IUserContext _userContext;
        private readonly IUserContextCacheReset _userContextCacheReset;
        private readonly string AESKey;

        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";
        private const string UseCaseFailMessage = "Falha ao atualizar o usuário, contate o administrador.";

        public UpdateUserUseCase(
            IRequestValidator<UpdateUserInput> validator,
            IUpdateUserRepository repository,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IUserContext userContext,
            IUserContextCacheReset userContextCacheReset)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _userContext = userContext;
            _userContextCacheReset = userContextCacheReset;

            AESKey = Environment.GetEnvironmentVariable(CypherAesKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {CypherAesKeyEnvironmentVariableName}");

        }

        public async Task<Output> Execute(UpdateUserInput input)
        {
            var currentUser = await _repository.GetUserById(input.UserId!);
            if (currentUser == null)
                return Output.CreateInvalidParametersResult("O usuário a ser atualizado não existe");

            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateUserUseCase), currentUser.Email);
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);           

            var uniqueDataVerificationResult = await VerifyIfThereAreUsersWithTheSameUniqueData(input);
            if (!uniqueDataVerificationResult.success)
                return Output.CreateInvalidParametersResult(uniqueDataVerificationResult.errorMessage);

            var result = await UpdateSqlServeUser(currentUser, input);
            if (!result)
                return Output.CreateInternalErrorResult(UseCaseFailMessage);

            var fireBaseSuccess = await UpdateFireBaseUser(input, currentUser);
            if (!fireBaseSuccess)
                return Output.CreateInternalErrorResult(UseCaseFailMessage);

            _userContextCacheReset.Reset(currentUser.Email!);
            return Output.CreateOkResult();
        }

        private async Task<bool> UpdateSqlServeUser(UserModel currentUserData, UpdateUserInput newUserData)
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

            return result.success;
        }

        private async Task<bool> UpdateFireBaseUser(UpdateUserInput newUser, UserModel oldUser)
        {
            try
            {
                var fireBaseInstance = FirebaseAuth.DefaultInstance;
                var userRecord = await fireBaseInstance.GetUserByEmailAsync(oldUser.Email);
                var args = new UserRecordArgs()
                {
                    Uid = userRecord.Uid,
                    Email = newUser.UserEmail,
                    Password = newUser.UserPassword,
                    DisplayName = $"{newUser.UserFirstName} {newUser.UserLastName} - {(int)newUser.UserPermission!}"
                };

                _ = await fireBaseInstance.UpdateUserAsync(args);
                return true;
            }
            catch
            {
                _ = await _repository.UpdateUser(oldUser);
                return false;
            }
        }

        private async Task<(bool success, string errorMessage)> VerifyIfThereAreUsersWithTheSameUniqueData(UpdateUserInput input)
        {
            var usersWithTheSamePhone = await _repository.GetUsersByPhone(input.UserPhone!);
            if (usersWithTheSamePhone.Count > 1 || usersWithTheSamePhone.Any(u => u.UserId != input.UserId))
                return (false, "Já existe um usuário com o telefone informado");

            var usersWithTheSameEmail = await _repository.GetUsersByEmail(input.UserEmail!);
            if (usersWithTheSameEmail.Count > 1 || usersWithTheSameEmail.Any(u => u.UserId != input.UserId))
                return (false, "Já existe um usuário com o email informado");

            var usersWithTheSameDocument = await _repository.GetUsersByDocument(input.UserDocument!);
            if (usersWithTheSameDocument.Count > 1 || usersWithTheSameDocument.Any(u => u.UserId != input.UserId))
                return (false, "Já existe um usuário com o documento informado");

            return (true, string.Empty);
        }
    }
}
