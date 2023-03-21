using System.Threading.Tasks;

namespace CTC.Application.Features.User.Services.Firebase
{
    internal interface IFirebaseService
    {
        Task<(bool sucess, string? token)> LoginInFireBase(string userEmail, string userPassword);

        Task<bool> DeleteFireBaseUser(string userEmail);

        Task RegisterFireBaseUser(string userPassword, string userEmail, string userDisplayName);
    }
}
