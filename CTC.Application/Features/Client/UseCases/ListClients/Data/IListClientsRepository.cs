using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.ListClients.Data
{
    internal interface IListClientsRepository
    {
        Task<PaginatedQueryResult<ClientModel>> ListClients(QueryRequest queryParams);
    }
}
