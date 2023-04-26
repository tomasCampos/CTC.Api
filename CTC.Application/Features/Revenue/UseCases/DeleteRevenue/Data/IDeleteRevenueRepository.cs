using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.DeleteRevenue.Data
{
    internal interface IDeleteRevenueRepository
    {
        Task<string> GetTransactionIdByRevenueId(string id);

        Task<bool> DeleteRevenue(string revenueId, string transactionId);
    }
}
