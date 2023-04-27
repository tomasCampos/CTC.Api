using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.ListExpenses.Data
{
    internal sealed class ListExpensesRepository : IListExpensesRepository
    {
        private readonly ISqlService _sqlService;

        public ListExpensesRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<PaginatedQueryResult<ExpenseModel>> ListExpenses(QueryRequest queryParams, string? costCenterName, string? categoryName, int? year)
        {
            var whereStatement = @"WHERE 1=1 ";
            if (!string.IsNullOrEmpty(costCenterName))
                whereStatement += $"AND cc.cost_center_name LIKE '%{costCenterName}%' ";
            if (!string.IsNullOrEmpty(categoryName))
                whereStatement += $"AND cat.category_name LIKE '%{categoryName}%' ";
            if (year.HasValue)
                whereStatement += $"AND YEAR(tran.transaction_payment_date) = {year} ";

            var result = await _sqlService.SelectPaginated<ExpenseModel>(queryParams, ExpenseSqlScripts.LIST_EXPENSE_SELECT_STATEMENT, 
                ExpenseSqlScripts.LIST_EXPENSE_FROM_AND_JOIN_STATEMENT, whereStatement, true);

            return result;
        }
    }
}
