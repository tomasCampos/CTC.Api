using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.DeleteClient.Data
{
    internal class DeleteClientRepository : IDeleteClientRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteClientRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> DeleteClient(string clientId, string personId)
        {
            var sqlCommands = BuildCommands(clientId, personId);
            var result = await _sqlService.ExecuteWithTransactionAsync(sqlCommands);

            return result.Success;
        }

        public async Task<ClientModel> GetClientById(string clientId)
        {
            var result = await _sqlService.SelectAsync<ClientModel>(ClientSqlScripts.GET_CLIENT_BY_ID, new { client_id = clientId });
            return result.FirstOrDefault();
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string clientId, string personId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    ClientSqlScripts.DELETE_CLIENT,
                    new
                    {
                        client_id = clientId
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
