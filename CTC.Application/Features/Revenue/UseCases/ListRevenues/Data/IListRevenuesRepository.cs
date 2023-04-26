using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.ListRevenues.Data
{
    internal interface IListRevenuesRepository
    {
        Task<PaginatedQueryResult<RevenueModel>> ListRevenues(QueryRequest queryParams, string? costCenterName, string? categoryName, int? year);
    }
}
