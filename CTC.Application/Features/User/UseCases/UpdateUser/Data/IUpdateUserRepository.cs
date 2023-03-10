using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.Data
{
    internal interface IUpdateUserRepository
    {
        Task<(bool success, int affectedRows)> UpdateUser(UserModel model);

        Task<UserModel> GetUserById(string userId);
    }
}
