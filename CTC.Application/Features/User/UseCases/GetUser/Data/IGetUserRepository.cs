using CTC.Application.Features.User.Models;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.GetUser.Data
{
    internal interface IGetUserRepository
    {
        Task<UserModel> GetUserByEmail(string email);
    }
}
