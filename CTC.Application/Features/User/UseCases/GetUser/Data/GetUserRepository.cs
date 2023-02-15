using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.GetUser.Data
{
    internal sealed class GetUserRepository : IGetUserRepository
    {
        private readonly ISqlService _sqlService;

        public GetUserRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<UserModel> GetUserByEmail(string email)
        {
            var user = await _sqlService.SelectAsync<UserModel>(GetUserSqlScripts.GetUserByEmail, new { person_email = email });
            return user.FirstOrDefault();
        }
    }
}
