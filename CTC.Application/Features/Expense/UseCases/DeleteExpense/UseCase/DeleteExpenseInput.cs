using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Expense.UseCases.DeleteExpense.UseCase
{
    public sealed class DeleteExpenseInput : IInput
    {
        public string? ExpenseId { get; set; }
    }
}
