using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.ListClients.Data
{
    internal sealed class ListClientsRepository : IListClientsRepository
    {
        private readonly ISqlService _sqlService;

        public ListClientsRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<PaginatedQueryResult<ClientModel>> ListClients(QueryRequest queryParams)
        {
            var result = await _sqlService.SelectPaginated<ClientModel>(queryParams, ListClientsSqlScripts.ListClientsSelectStatement, ListClientsSqlScripts.ListClientsFromAndJoinsStatements,
                                    ListClientsSqlScripts.ListClientsWhereStatement);

            return result;
        }
    }
}
