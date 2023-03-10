using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.Data
{
    internal class UpdateUserRepository : IUpdateUserRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateUserRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public Task<(bool success, int affectedRows)> UpdateUser(UserModel model)
        {
            var commands = BuildCommands(model);

            var result = _sqlService.ExecuteWithTransactionAsync(commands);
            return result;
        }

        public async Task<UserModel> GetUserById(string userId)
        {
            var user = await _sqlService.SelectAsync<UserModel>(UpdateUserSqlScripts.GET_USER_BY_ID_SQL_SCRIPT, new { user_id = userId });
            return user.FirstOrDefault();
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(UserModel model)
        {
            var updatePersonSetStatement = "`person_first_name` = @person_first_name";

            if(!string.IsNullOrWhiteSpace(model.Email))
                updatePersonSetStatement += "`,person_email` = @person_email";

            if (!string.IsNullOrWhiteSpace(model.Phone))
                updatePersonSetStatement += "`,person_phone` = @person_phone";

            if (!string.IsNullOrWhiteSpace(model.Document))
                updatePersonSetStatement += "`,person_document` = @person_document";

            var updatePersonSqlCommand = PersonSqlScripts.GetUpdatePersonSql(updatePersonSetStatement);

            var commands = new Dictionary<string, object?>
            {
                {
                    updatePersonSqlCommand,
                    new
                    {
                        person_id = model.PersonId,
                        person_first_name = model.FirstName,
                        person_email = model.Email,
                        person_phone = model.Phone,
                        person_document = model.Document
                    }
                },

                {
                    UpdateUserSqlScripts.UPDATE_USER_SQL,
                    new
                    {
                        user_id = model.UserId,
                        user_last_name = model.LastName,
                        user_permission = (int)model.Permission!,
                        user_password = model.Password,
                    }
                }
            };

            return commands;
        }

        #endregion
    }
}