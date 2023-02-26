using CTC.Application.Shared.Authorization;

namespace CTC.Api.Auth.Services
{
    public interface IUserAuthorizationService
    {
        Task<UserPermission> GetUserPermission(string userEmail);
    }
}
