using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.GetRevenue.Data
{
    internal sealed class GetRevenueRepository : IGetRevenueRepository
    {
        ISqlService _sqlService;

        public GetRevenueRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<RevenueModel> GetRevenue(string id)
        {
            var expense = await _sqlService.SelectAsync<RevenueModel>(RevenueSqlScripts.SELECT_REVENUE_BY_ID, new { revenue_id = id });
            return expense.FirstOrDefault();
        }
    }
}
