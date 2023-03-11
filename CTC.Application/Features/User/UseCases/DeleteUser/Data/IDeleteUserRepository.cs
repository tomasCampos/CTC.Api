using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.DeleteUser.Data
{
    internal interface IDeleteUserRepository
    {
        Task<bool> DeleteUser(string userId, string personId);

        Task<UserModel> GetUserById(string userId);
    }
}