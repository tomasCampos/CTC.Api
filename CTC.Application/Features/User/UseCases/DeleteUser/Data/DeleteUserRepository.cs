using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.DeleteUser.Data
{
    internal class DeleteUserRepository : IDeleteUserRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteUserRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> DeleteUser(string userId, string personId)
        {
            var sqlCommands = BuildCommands(userId, personId);
            var result = await _sqlService.ExecuteWithTransactionAsync(sqlCommands);

            return result.Success;
        }

        public async Task<UserModel> GetUserById(string userId)
        {
            var result = await _sqlService.SelectAsync<UserModel>(UserSqlScripts.GET_USER_BY_ID, new { user_id = userId });
            return result.FirstOrDefault();
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string userId, string personId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    UserSqlScripts.DELETE_USER,
                    new
                    {
                        user_id = userId
                    }
                },

                {
                    PersonSqlScripts.DELETE_PERSON_SQL,
                    new
                    {
                        person_id = personId
                    }
                }                
            };

            return commands;
        }

        #endregion
    }
}
