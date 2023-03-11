using System.Threading.Tasks;

namespace CTC.Application.Shared.UserContext.Services
{
    public interface IUserContextService
    {
        Task SetUserContext(string userEmail, string bearerToken);
    }
}
