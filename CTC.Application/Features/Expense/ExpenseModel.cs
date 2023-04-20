using CTC.Application.Shared.Models.Transaction;
using System;

namespace CTC.Application.Features.Expense
{
    internal sealed class ExpenseModel : TransactionModel
    {
        public ExpenseModel(in string expenseId, in string supplierId, in string transactionId, in decimal value, 
                            in DateTime paymentDate, in string observation, in string categoryId, in string costCenterId) 
            : base(transactionId ?? Guid.NewGuid().ToString(), value, paymentDate, observation, categoryId, costCenterId)
        {
            ExpenseId = expenseId;
            SupplierId = supplierId;
        }

        public string? ExpenseId { get; set; }
        public string? SupplierId { get; set; }
    }
}
