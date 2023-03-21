using CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.Services.Firebase
{
    internal interface IFirebaseService
    {
        Task<(bool sucess, string? token)> LoginInFireBase(string userEmail, string userPassword);
    }
}
