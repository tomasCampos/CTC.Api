using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.ListCostCenter.Data
{
    internal sealed class ListCostCentersRepository : IListCostCentersRepository
    {
        private readonly ISqlService _sqlService;

        public ListCostCentersRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<PaginatedQueryResult<CostCenterModel>> ListCostCenters(QueryRequest queryParams)
        {
            var result = await _sqlService.SelectPaginated<CostCenterModel>(queryParams, CostCenterSqlScripts.LIST_COST_CENTER_SELECT_STATEMENT, 
                CostCenterSqlScripts.LIST_COST_CENTER_FROM_AND_JOIN_STATEMENT, CostCenterSqlScripts.LIST_COST_CENTER_WHERE_STATEMENT);

            return result;
        }
    }
}
