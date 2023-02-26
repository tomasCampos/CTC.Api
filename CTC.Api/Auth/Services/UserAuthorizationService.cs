﻿using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Api.Auth.Services
{
    public class UserAuthorizationService : IUserAuthorizationService
    {
        private readonly IUseCase<IGetUserInput, Output> _getUserUseCase;

        private static readonly IDictionary<string, UserPermission> UserPermissionsCache = new Dictionary<string, UserPermission>();

        public UserAuthorizationService(IUseCase<IGetUserInput, Output> getUserUseCase)
        {
            _getUserUseCase = getUserUseCase;
        }

        public async Task<UserPermission> GetUserPermission(string userEmail)
        {
            var isInCache = UserPermissionsCache.TryGetValue(userEmail, out UserPermission userPermission);
            if (isInCache)
                return userPermission;

            var user = await _getUserUseCase.Execute(new GetUserByEmailInput(userEmail, UserPermission.Administrator));
            UserPermission permission = (UserPermission)(user.Body?.GetType().GetProperty("Permission")?.GetValue(user.Body, null))!;
            UserPermissionsCache.Add(userEmail, permission);

            return permission;
        }
    }
}
