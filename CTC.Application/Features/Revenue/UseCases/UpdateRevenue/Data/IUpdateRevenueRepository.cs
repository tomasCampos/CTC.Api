using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.UpdateRevenue.Data
{
    internal interface IUpdateRevenueRepository
    {
        Task<string> GetClientIdByCostCenterId(string costCenterId);
        Task<string> GetTransactionIdByRevenueId(string id);
        Task<bool> UpdateRevenue(RevenueModel revenue);
    }
}
