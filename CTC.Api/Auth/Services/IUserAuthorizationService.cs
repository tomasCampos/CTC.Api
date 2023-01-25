using CTC.Application.Shared.Request;

namespace CTC.Api.Auth.Services
{
    public interface IUserAuthorizationService
    {
        Task<UserPermission> GetUserPermission(string userEmail);
    }
}
