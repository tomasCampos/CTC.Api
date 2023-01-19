using CTC.Application.Features.User.Models;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.RegisterUser.Repositories
{
    internal class RegisterUserRepository : IRegisterUserRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterUserRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> InsertUser(UserModel model)
        {
            var commands = BuildCommands(model);

            return await _sqlService.ExecuteWithTransactionAsync(commands);
        }

        private static Dictionary<string, object?> BuildCommands(UserModel model)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    PersonSqlScripts.InsertPersonSql,
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
                    RegisterUserSqlScripts.INSERT_USER_SQL,
                    new
                    {
                        user_id = model.UserId,
                        user_last_name = model.LastName,
                        user_permission = model.Permission,
                        user_password = model.Password,
                        person_id = model.PersonId
                    }
                }
            };
            return commands;
        }
    }
}