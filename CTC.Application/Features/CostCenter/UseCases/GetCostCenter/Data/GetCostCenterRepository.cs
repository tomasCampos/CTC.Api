using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenter.Data
{
    internal class GetCostCenterRepository : IGetCostCenterRepository
    {
        private readonly ISqlService _sqlService;

        public GetCostCenterRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<CostCenterModel> GetCostcenter(string id)
        {
            var costCenter = await _sqlService.SelectAsync<CostCenterModel>(CostCenterSqlScripts.SELECT_COST_CENTER_BY_ID, new { cost_center_id = id });
            return costCenter.FirstOrDefault();
        }
    }
}
