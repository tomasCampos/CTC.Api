using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UserContext;

namespace CTC.Api.Auth.Services
{
    public class UserContextService : IUserAuthorizationService
    {
        private readonly IUseCase<IGetUserInput, Output> _getUserUseCase;
        private readonly IUserContextSet _userContextSet;

        private static readonly IDictionary<string, UserContextModel?> UserPermissionsCache = new Dictionary<string, UserContextModel?>();

        public UserContextService(IUseCase<IGetUserInput, Output> getUserUseCase, IUserContextSet userContextSet)
        {
            _getUserUseCase = getUserUseCase;
            _userContextSet = userContextSet;
        }

        public async Task SetUserContext(string userEmail, string bearerToken)
        {
            var isInCache = UserPermissionsCache.TryGetValue(userEmail, out UserContextModel? userModel);
            if (isInCache && userModel is not null)
            {
                _userContextSet.Set(userModel.Name, userModel.Email, userModel.Permission, userModel.Phone, userModel.Document, bearerToken);
                return;
            }

            var user = await _getUserUseCase.Execute(new GetUserByEmailInput(userEmail));
            userModel = CreateUserContextModel(user);
            UserPermissionsCache.Add(userEmail, userModel);

            _userContextSet.Set(userModel.Name, userModel.Email, userModel.Permission, userModel.Phone, userModel.Document, bearerToken);
        }

        private static UserContextModel CreateUserContextModel(Output user)
        {
            UserPermission permission = (UserPermission)(user.Body?.GetType().GetProperty("Permission")?.GetValue(user.Body, null))!;
            string firstName = (string)user.Body?.GetType().GetProperty("FirstName")?.GetValue(user.Body, null)!;
            string lastName = (string)user.Body?.GetType().GetProperty("LastName")?.GetValue(user.Body, null)!;
            string email = (string)user.Body?.GetType().GetProperty("Email")?.GetValue(user.Body, null)!;
            string phone = (string)user.Body?.GetType().GetProperty("Phone")?.GetValue(user.Body, null)!;
            string document = (string)user.Body?.GetType().GetProperty("Document")?.GetValue(user.Body, null)!;

            return new UserContextModel($"{firstName} {lastName}", email, phone, document, permission);
        }
    }
}
