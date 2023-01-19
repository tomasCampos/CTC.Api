using CTC.Application.Features.User.Models;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.RegisterUser.Repositories
{
    internal interface IRegisterUserRepository
    {
        public Task<bool> InsertUser(UserModel model);
    }
}
