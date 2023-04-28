using System;

namespace CTC.Application.Features.Analytics.Data
{
    internal sealed class TransactionAnalyticsModel
    {
        public string? CostCenterId { get; set; }
        public string? TransactionId { get; set; }
        public decimal TransactionValue { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}
