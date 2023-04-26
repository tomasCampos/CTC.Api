using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.UpdateExpense.Data
{
    internal interface IUpdateExpenseRepository
    {
        Task<string> GetTransactionIdByExpenseId(string id);

        Task<bool> UpdateExpense(ExpenseModel expense);

        Task<bool> VerifyIfSupplierExists(string supplierId);
    }
}
