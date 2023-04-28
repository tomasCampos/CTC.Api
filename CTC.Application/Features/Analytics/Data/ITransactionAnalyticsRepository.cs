using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.Data
{
    internal interface ITransactionAnalyticsRepository
    {
        Task<IEnumerable<TransactionAnalyticsModel>> ListExpensesByYear(int year, TransactionAnalyticsFiltersType filterType);
        Task<IEnumerable<TransactionAnalyticsModel>> ListRevenuesByYear(int year, TransactionAnalyticsFiltersType filterType);
    }
}
