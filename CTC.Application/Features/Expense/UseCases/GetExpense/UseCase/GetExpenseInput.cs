using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Expense.UseCases.GetExpense.UseCase
{
    public sealed class GetExpenseInput : IInput
    {
        public string? ExpenseId { get; set; }
    }
}
