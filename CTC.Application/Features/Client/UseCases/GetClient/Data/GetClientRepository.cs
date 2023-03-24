using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.GetClient.Data
{
    internal class GetClientRepository : IGetClientRepository
    {
        private readonly ISqlService _sqlService;

        public GetClientRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<ClientModel> GetClientById(string clientId)
        {
            var result = await _sqlService.SelectAsync<ClientModel>(ClientSqlScripts.GET_CLIENT_BY_ID, new { client_id = clientId });
            return result.FirstOrDefault();
        }
    }
}
