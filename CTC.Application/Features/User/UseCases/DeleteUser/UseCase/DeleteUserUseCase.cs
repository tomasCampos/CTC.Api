using CTC.Application.Features.User.Services.Firebase;
using CTC.Application.Features.User.UseCases.DeleteUser.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Cypher;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UserContext.Services;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.DeleteUser.UseCase
{
    internal sealed class DeleteUserUseCase : IUseCase<DeleteUserInput, Output>
    {
        private readonly IRequestValidator<DeleteUserInput> _validator;
        private readonly IDeleteUserRepository _deleteUserRepository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IUserContextCacheReset _userContextCacheReset;
        private readonly IFirebaseService _firebaseService;
        private readonly string AESKey;

        private const string CypherAesKeyEnvironmentVariableName = "CYPHER_AES_KEY";
        private const string ErrorMessage = "Falha ao excluir usuário. Contate o administrador";

        public DeleteUserUseCase(
            IRequestValidator<DeleteUserInput> validator,
            IDeleteUserRepository deleteUserRepository,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IUserContextCacheReset userContextCacheReset,
            IFirebaseService firebaseService)
        {
            _validator = validator;
            _deleteUserRepository = deleteUserRepository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _userContextCacheReset = userContextCacheReset;
            _firebaseService = firebaseService;

            AESKey = Environment.GetEnvironmentVariable(CypherAesKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {CypherAesKeyEnvironmentVariableName}");
        }

        public async Task<Output> Execute(DeleteUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteUserUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var user = await _deleteUserRepository.GetUserById(input.UserId!);
            if (user == null)
                return Output.CreateInvalidParametersResult("O usuário a ser excluído não existe.");

            var deleteUserFireBaseResult = await _firebaseService.DeleteFireBaseUser(user.Email!);

            if (!deleteUserFireBaseResult)
                return Output.CreateInternalErrorResult(ErrorMessage);

            var deleteUserInSqlResult = await _deleteUserRepository.DeleteUser(user.UserId!, user.PersonId!);

            if (!deleteUserInSqlResult)
            {
                await _firebaseService.RegisterFireBaseUser(AES.Decrypt(user.Password!, AESKey), user.Email!, $"{user.FirstName} {user.LastName} - {(int)user.Permission!}");
                return Output.CreateInternalErrorResult(ErrorMessage);
            }

            _userContextCacheReset.Reset(user.Email!);
            return Output.CreateOkResult();
        }
    }
}