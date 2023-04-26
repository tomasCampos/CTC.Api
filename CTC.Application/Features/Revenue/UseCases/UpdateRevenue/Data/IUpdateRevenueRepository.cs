using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.UpdateRevenue.Data
{
    internal interface IUpdateRevenueRepository
    {
        Task<string> GetTransactionIdByRevenueId(string id);
        Task<bool> UpdateRevenue(RevenueModel revenue);
    }
}
