using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.DeleteExpense.Data
{
    internal interface IDeleteExpenseRepository
    {
        Task<string> GetTransactionIdByExpenseId(string id);

        Task<bool> DeleteExpense(string expenseId, string transactionId);
    }
}
