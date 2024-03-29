﻿using CTC.Application.Features.User.Services.Firebase;
using CTC.Application.Features.User.UseCases.UpdateUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request.Validator;
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
        private readonly IFirebaseService _firebaseService;
        private readonly string AESKey;

        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";
        private const string UseCaseFailMessage = "Falha ao atualizar o usuário, contate o administrador.";

        public UpdateUserUseCase(
            IRequestValidator<UpdateUserInput> validator,
            IUpdateUserRepository repository,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IUserContext userContext,
            IUserContextCacheReset userContextCacheReset,
            IFirebaseService firebaseService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _userContext = userContext;
            _userContextCacheReset = userContextCacheReset;
            _firebaseService = firebaseService;

            AESKey = Environment.GetEnvironmentVariable(CypherAesKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {CypherAesKeyEnvironmentVariableName}");

        }

        public async Task<Output> Execute(UpdateUserInput input)
        {   
            (var currentUser, var isProfileUdate) = await GetCurrentUser(input.UserId);
            if (currentUser == null)
                return Output.CreateInvalidParametersResult("O usuário a ser atualizado não existe");

            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateUserUseCase), currentUser.Email);
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            //Caso seja atualização de perfil (não passa o ID, o usuário a ser atualizado é obtido através do _userContext (usuário logado).)
            if (isProfileUdate)
            {
                //Nesse caso é necessário manter as informações a serem atualizadas com a permissão e o id, pois não são enviados pelo usuário e são necessários.
                input.UserPermission = currentUser.Permission;
                input.UserId = currentUser.UserId;
            }

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

        private async Task<(UserModel userModel, bool isProfileUpdate)> GetCurrentUser(string? userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                var users = await _repository.GetUsersByEmail(_userContext.UserEmail);
                return (users.FirstOrDefault(), true);
            }
            return (await _repository.GetUserById(userId), false);
        }

        private async Task<bool> UpdateSqlServeUser(UserModel currentUserData, UpdateUserInput newUserData)
        {
            string? newUserDocument = newUserData.UserDocument == currentUserData.Document ? string.Empty : newUserData.UserDocument;
            string? newUserPhone = newUserData.UserPhone == currentUserData.Phone ? string.Empty : newUserData.UserPhone;
            string? newUserEmail = newUserData.UserEmail == currentUserData.Email ? string.Empty : newUserData.UserEmail;
            string? newUserPassword = string.IsNullOrWhiteSpace(newUserData.UserPassword) ? currentUserData.Password : AES.Encrypt(newUserData.UserPassword!, AESKey);

            UserModel newUser;
            if (_userContext.UserPermission != UserPermission.Administrator)
                newUser = new UserModel(currentUserData.UserId!, currentUserData.PersonId!, newUserData.UserFirstName!, newUserEmail!, newUserPhone!, newUserData.UserLastName!, newUserPassword!, (int)currentUserData.Permission!, newUserDocument!);
            else
                newUser = new UserModel(currentUserData.UserId!, currentUserData.PersonId!, newUserData.UserFirstName!, newUserEmail!, newUserPhone!, newUserData.UserLastName!, newUserPassword!, (int)newUserData.UserPermission!, newUserDocument!);

            var result = await _repository.UpdateUser(newUser);

            return result.success;
        }

        private async Task<bool> UpdateFireBaseUser(UpdateUserInput newUser, UserModel oldUser)
        {
            try
            {
                string? newUserPassword = string.IsNullOrWhiteSpace(newUser.UserPassword) ? AES.Decrypt(oldUser.Password!, AESKey) : newUser.UserPassword;
                var userRecord = await _firebaseService.GetFirebaseUserByEmail(oldUser.Email!);
                var args = new UserRecordArgs()
                {
                    Uid = userRecord.Uid,
                    Email = newUser.UserEmail,
                    Password = newUserPassword,
                    DisplayName = $"{newUser.UserFirstName} {newUser.UserLastName} - {(int)newUser.UserPermission!}"
                };

                await _firebaseService.UpdateFireBaseUser(args);
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
