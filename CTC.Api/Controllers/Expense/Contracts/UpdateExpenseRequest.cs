﻿namespace CTC.Api.Controllers.Expense.Contracts
{
    public sealed class UpdateExpenseRequest
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
