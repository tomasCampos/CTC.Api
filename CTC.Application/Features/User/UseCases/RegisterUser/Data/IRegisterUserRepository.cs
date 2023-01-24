using CTC.Application.Features.User.Models;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.RegisterUser.Data
{
    internal interface IRegisterUserRepository
    {
        Task<bool> InsertUser(UserModel model);
    }
}
