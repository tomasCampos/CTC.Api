using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.GetExpense.Data
{
    internal interface IGetExpenseRepository
    {
        Task<ExpenseModel> GetExpense(string id);
    }
}
