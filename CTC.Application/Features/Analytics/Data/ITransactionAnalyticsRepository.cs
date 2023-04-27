using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.Data
{
    internal interface ITransactionAnalyticsRepository
    {
        Task<TransactionAnalyticsModel> ListExpensesByYear(int year, TransactionAnalyticsFiltersType filterType);
        Task<TransactionAnalyticsModel> ListRevenuesByYear(int year, TransactionAnalyticsFiltersType filterType);
    }
}
