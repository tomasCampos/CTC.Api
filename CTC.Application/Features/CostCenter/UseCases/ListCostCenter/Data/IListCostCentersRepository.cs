using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.ListCostCenter.Data
{
    internal interface IListCostCentersRepository
    {
        Task<PaginatedQueryResult<CostCenterModel>> ListCostCenters(QueryRequest queryParams);
    }
}
