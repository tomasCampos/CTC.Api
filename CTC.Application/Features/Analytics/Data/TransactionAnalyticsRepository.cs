using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.Data
{
    internal sealed class TransactionAnalyticsRepository : ITransactionAnalyticsRepository
    {
        public Task<TransactionAnalyticsModel> ListExpensesByYear(int year, TransactionAnalyticsFiltersType filterType)
        {
            throw new System.NotImplementedException();
        }

        public Task<TransactionAnalyticsModel> ListRevenuesByYear(int year, TransactionAnalyticsFiltersType filterType)
        {
            throw new System.NotImplementedException();
        }

        private static string GetWhereClauseByFilterType(TransactionAnalyticsFiltersType filterType) 
        {
            switch (filterType) 
            {
                case TransactionAnalyticsFiltersType.OfYear : return "WHERE YEAR(tran.transaction_payment_date) == @transaction_payment_date";
                case TransactionAnalyticsFiltersType.BeforeOrEqualsToYear : return "WHERE YEAR(tran.transaction_payment_date) <= @transaction_payment_date";
                default: return "";
            }
        }
    }
}
