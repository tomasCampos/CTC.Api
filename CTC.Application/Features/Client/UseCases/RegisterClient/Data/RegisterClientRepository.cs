using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.RegisterClient.Data
{
    internal sealed class RegisterClientRepository : IRegisterClientRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterClientRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> InsertClient(ClientModel model)
        {
            var commands = BuildCommands(model);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        public async Task<int> VerifyIfClientAlreadyExists(string email, string phone, string document)
        {
            return await _sqlService.CountAsync(ClientSqlScripts.COUNT_CLIENT_BY_EMAIL_PHONE_DOCUMENT, new { person_email = email, person_phone = phone, person_document = document });
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(ClientModel model)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    PersonSqlScripts.INSERT_PERSON_SQL,
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
                    ClientSqlScripts.INSERT_CLIENT,
                    new
                    {
                        client_id = model.ClientId,
                        person_id = model.PersonId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
