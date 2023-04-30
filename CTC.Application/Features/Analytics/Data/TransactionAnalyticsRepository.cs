using CTC.Application.Shared.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.Data
{
    internal sealed class TransactionAnalyticsRepository : ITransactionAnalyticsRepository
    {
        private const string SELECT_EXPENSES = @"SELECT 
	                                                cc.cost_center_id AS `CostCenterId`,
                                                    cc.cost_center_name AS `CostCenterName`,
	                                                tran.transaction_id AS `TransactionId`,
                                                    tran.transaction_value AS `TransactionValue`,
                                                    tran.transaction_payment_date AS `PaymentDate`
                                                FROM  
	                                                `heroku_3a06699194dd49a`.expense ex
                                                INNER JOIN 
	                                                `heroku_3a06699194dd49a`.transaction tran ON ex.transaction_id = tran.transaction_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id";

        private const string SELECT_REVENUES = @"SELECT 
	                                                cc.cost_center_id AS `CostCenterId`,
                                                    cc.cost_center_name AS `CostCenterName`,
	                                                tran.transaction_id AS `TransactionId`,
                                                    tran.transaction_value AS `TransactionValue`,
                                                    tran.transaction_payment_date AS `PaymentDate`
                                                FROM  
	                                                `heroku_3a06699194dd49a`.revenue rev
                                                INNER JOIN 
	                                                `heroku_3a06699194dd49a`.transaction tran ON rev.transaction_id = tran.transaction_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id";

        private readonly ISqlService _sqlService;

        public TransactionAnalyticsRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<(IEnumerable<TransactionAnalyticsModel> expensesData, IEnumerable<TransactionAnalyticsModel> revenuesData)> ListTransactionsByYear(int year, TransactionAnalyticsFiltersType filterType)
        {
            var whereClause = GetWhereClauseByFilterType(filterType);
            var sqlExpenses = $"{SELECT_EXPENSES} {whereClause}";
            var sqlRevenues = $"{SELECT_REVENUES} {whereClause}";

            var expensesTask = _sqlService.SelectAsync<TransactionAnalyticsModel>(sqlExpenses, new { transaction_payment_year = year });
            var revenueTask = _sqlService.SelectAsync<TransactionAnalyticsModel>(sqlRevenues, new { transaction_payment_year = year });
            await Task.WhenAll(expensesTask, revenueTask);

            return(expensesTask.Result, revenueTask.Result);
        }

        private static string GetWhereClauseByFilterType(TransactionAnalyticsFiltersType filterType) 
        {
            switch (filterType) 
            {
                case TransactionAnalyticsFiltersType.EqualsToYear : return "WHERE YEAR(tran.transaction_payment_date) = @transaction_payment_year";
                case TransactionAnalyticsFiltersType.BeforeOrEqualsToYear : return "WHERE YEAR(tran.transaction_payment_date) <= @transaction_payment_year";
                default: return "";
            }
        }
    }
}
