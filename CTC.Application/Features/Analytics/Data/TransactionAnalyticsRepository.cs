using CTC.Application.Shared.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Analytics.Data
{
    internal sealed class TransactionAnalyticsRepository : ITransactionAnalyticsRepository
    {
        private const string SELECT_EXPENSES = @"SELECT 
	                                                cc.cost_center_id AS `CostCenterId`,
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

        public async Task<IEnumerable<TransactionAnalyticsModel>> ListExpensesByYear(int year, TransactionAnalyticsFiltersType filterType)
        {
            var whereClause = GetWhereClauseByFilterType(filterType);
            var sql = $"{SELECT_EXPENSES} {whereClause}";
            return await _sqlService.SelectAsync<TransactionAnalyticsModel>(sql, new { transaction_payment_year = year });
        }

        public async Task<IEnumerable<TransactionAnalyticsModel>> ListRevenuesByYear(int year, TransactionAnalyticsFiltersType filterType)
        {
            var whereClause = GetWhereClauseByFilterType(filterType);
            var sql = $"{SELECT_REVENUES} {whereClause}";
            return await _sqlService.SelectAsync<TransactionAnalyticsModel>(sql, new { transaction_payment_year = year });
        }

        private static string GetWhereClauseByFilterType(TransactionAnalyticsFiltersType filterType) 
        {
            switch (filterType) 
            {
                case TransactionAnalyticsFiltersType.OfYear : return "WHERE YEAR(tran.transaction_payment_date) == @transaction_payment_year";
                case TransactionAnalyticsFiltersType.BeforeOrEqualsToYear : return "WHERE YEAR(tran.transaction_payment_date) <= @transaction_payment_year";
                default: return "";
            }
        }
    }
}
