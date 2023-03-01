namespace CTC.Api.Auth.Services
{
    public interface IUserAuthorizationService
    {
        Task SetUserContext(string userEmail);
    }
}
