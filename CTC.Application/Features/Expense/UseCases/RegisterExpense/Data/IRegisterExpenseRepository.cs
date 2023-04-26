using System.Threading.Tasks;

namespace CTC.Application.Features.Expense.UseCases.RegisterExpense.Data
{
    internal interface IRegisterExpenseRepository
    {
        Task<bool> InsertExpense(ExpenseModel model);

        Task<bool> VerifyIfSupplierExists(string supplierId);
    }
}