using CTC.Application.Features.User.Models;

namespace CTC.Api.Auth.Services
{
    public interface IUserAuthorizationService
    {
        Task<UserPermission> GetUserPermission(string userEmail);
    }
}
