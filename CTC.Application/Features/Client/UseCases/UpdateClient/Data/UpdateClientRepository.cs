using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.UpdateClient.Data
{
    internal sealed class UpdateClientRepository : IUpdateClientRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateClientRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<ClientModel> GetClientById(string id)
        {
            var client = await _sqlService.SelectAsync<ClientModel>(UpdateClientSqlScripts.GET_CLIENT_BY_ID_SQL, new { client_id = id });
            return client.FirstOrDefault();
        }

        public async Task<List<ClientModel>> GetClientsByDocument(string document)
        {
            var sql = UpdateClientSqlScripts.GetSelectClientQuery("WHERE p.person_email = @person_email");
            var result = await _sqlService.SelectAsync<ClientModel>(sql, new { person_document = document });
            return result.ToList();
        }

        public async Task<List<ClientModel>> GetClientsByEmail(string email)
        {
            var sql = UpdateClientSqlScripts.GetSelectClientQuery("WHERE p.person_email = @person_email");
            var result = await _sqlService.SelectAsync<ClientModel>(sql, new { person_email = email });
            return result.ToList();
        }

        public async Task<List<ClientModel>> GetClientsByPhone(string phone)
        {
            var sql = UpdateClientSqlScripts.GetSelectClientQuery("WHERE p.person_email = @person_email");
            var result = await _sqlService.SelectAsync<ClientModel>(sql, new { person_phone = phone });
            return result.ToList();
        }

        public async Task<int> UpdateClient(ClientModel model)
        {
            var updatePersonSqlCommand = PersonSqlScripts.GetUpdatePersonSql(model);

            var result = await _sqlService.ExecuteAsync(updatePersonSqlCommand, new
            {
                person_id = model.PersonId,
                person_first_name = model.FirstName,
                person_email = model.Email,
                person_phone = model.Phone,
                person_document = model.Document
            });

            return result;
        }
    }
}
