using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.Data
{
    internal interface IUpdateUserRepository
    {
        Task<(bool success, int affectedRows)> UpdateUser(UserModel model);

        Task<UserModel> GetUserById(string userId);

        Task<List<UserModel>> GetUsersByEmail(string email);

        Task<List<UserModel>> GetUsersByDocument(string document);

        Task<List<UserModel>> GetUsersByPhone(string phone);
    }
}
