using CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data.Models;
using CTC.Application.Shared.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data
{
    internal sealed class GetCostCenterReportRepository : IGetCostCenterReportRepository
    {
        private readonly ISqlService _sqlService;

        private const string SELECT_EXPENSES = @"SELECT
                                                    'Expense' AS `TransactionType`,
	                                                tran.transaction_value AS `TransactionValue`,
	                                                tran.transaction_payment_date AS `PaymentDate`,
	                                                cat.category_name AS `CategoryName`,
	                                                per.person_first_name AS `SupplierName`,
	                                                per.person_document AS `SupplierDocument`
                                                FROM  
	                                                `heroku_3a06699194dd49a`.expense ex
                                                INNER JOIN 
	                                                `heroku_3a06699194dd49a`.transaction tran ON ex.transaction_id = tran.transaction_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.cost_center cc ON tran.cost_center_id = cc.cost_center_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.supplier sup ON ex.supplier_id = sup.supplier_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.person per ON sup.person_id = per.person_id
                                                LEFT JOIN
	                                                `heroku_3a06699194dd49a`.category cat ON tran.category_id = cat.category_id
                                                WHERE 
	                                                cc.cost_center_id = @cost_center_id";

        private const string SELECT_REVENUES = @"SELECT
                                                    'Revenue' AS `TransactionType`,
	                                                tran.transaction_value AS `TransactionValue`,
	                                                tran.transaction_payment_date AS `PaymentDate`,
	                                                cat.category_name AS `CategoryName`
                                                FROM  
	                                                `heroku_3a06699194dd49a`.revenue rev
                                                INNER JOIN 
	                                                `heroku_3a06699194dd49a`.transaction tran ON tran.transaction_id = rev.transaction_id
                                                INNER JOIN
	                                                `heroku_3a06699194dd49a`.cost_center cc ON cc.cost_center_id = tran.cost_center_id
                                                LEFT JOIN
	                                                `heroku_3a06699194dd49a`.category cat ON tran.category_id = cat.category_id
                                                WHERE 
	                                                cc.cost_center_id = @cost_center_id";

        public GetCostCenterReportRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<CostCenterModel> GetCostCenter(string id)
        {
            var costCenter = await _sqlService.SelectAsync<CostCenterModel>(CostCenterSqlScripts.SELECT_COST_CENTER_BY_ID, new { cost_center_id = id });
            return costCenter.FirstOrDefault();
        }

        public async Task<IEnumerable<CostCenterTransactionsModel>> ListTransactionsByCostCenterId(string id)
        {
            var expensesTask = _sqlService.SelectAsync<CostCenterTransactionsModel>(SELECT_EXPENSES, new { cost_center_id = id });
            var revenuesTask = _sqlService.SelectAsync<CostCenterTransactionsModel>(SELECT_REVENUES, new { cost_center_id = id });
            await Task.WhenAll(expensesTask, revenuesTask);

            var expenses = expensesTask.Result;
            var revenues = revenuesTask.Result;
            var result = expenses.Concat(revenues).OrderBy(tran => tran.TransactionType);

            return result;
        }
    }
}
