using CTC.Application.Shared.Models.Transaction;
using System;

namespace CTC.Application.Features.Expense
{
    internal sealed class ExpenseModel : TransactionModel
    {
        public ExpenseModel(in string supplierId, in decimal value, in DateTime? paymentDate, in string? observation, in string? categoryId, in string costCenterId,
            in string? categoryName = null, in string? expenseId = null, in string? transactionId = null, in string? supplierName = null, in string? costCenterName = null) 
            : base(transactionId ?? Guid.NewGuid().ToString(), value, paymentDate, observation, categoryId, costCenterId, categoryName, costCenterName)
        {
            ExpenseId = expenseId ?? Guid.NewGuid().ToString();
            SupplierId = supplierId;
            SupplierName = supplierName;
        }

        public ExpenseModel() {}

        public string? ExpenseId { get; set; }
        public string? SupplierId { get; set; }
        public string? SupplierName { get; set; }
    }
}
