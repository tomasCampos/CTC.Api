using CTC.Application.Shared.Data;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.GetExpense.Data
{
    internal sealed class GetExpenseRepository : IGetExpenseRepository
    {
        ISqlService _sqlService;

        public GetExpenseRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<ExpenseModel> GetExpense(string id)
        {
            var expense = await _sqlService.SelectAsync<ExpenseModel>(ExpenseSqlScripts.SELECT_EXPENSE_BY_ID, new { expense_id = id });
            return expense.FirstOrDefault();
        }
    }
}
