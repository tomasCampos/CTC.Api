using System;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.Data.Models
{
    internal sealed class CostCenterTransactionsModel
    {
        public string? TransactionType { get; set; }
        public decimal? TransactionValue { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? CategoryName { get; set; }
        public string? SupplierName { get; set; }
        public string? SupplierDocument { get; set; }
    }
}
