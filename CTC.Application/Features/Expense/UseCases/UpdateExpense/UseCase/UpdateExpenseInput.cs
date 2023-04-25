using CTC.Application.Shared.UseCase.IO;
using System;

namespace CTC.Application.Features.Expense.UseCases.UpdateExpense.UseCase
{
    public sealed class UpdateExpenseInput : IInput
    {
        public string? ExpenseId { get; set; }
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Observation { get; set; }
        public string? CategoryId { get; set; }
        public string? CostCenterId { get; set; }
        public string? SupplierId { get; set; }
    }
}