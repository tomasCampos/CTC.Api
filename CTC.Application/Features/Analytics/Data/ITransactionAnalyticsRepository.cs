using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.Data
{
    internal interface ITransactionAnalyticsRepository
    {
        Task<(IEnumerable<TransactionAnalyticsModel> expensesData, IEnumerable<TransactionAnalyticsModel> revenuesData)> ListTransactionsByYear(int year, TransactionAnalyticsFiltersType filterType);
    }
}
