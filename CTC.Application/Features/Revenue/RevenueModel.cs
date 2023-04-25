using CTC.Application.Shared.Models.Transaction;
using System;

namespace CTC.Application.Features.Revenue
{
    internal sealed class RevenueModel : TransactionModel
    {
        public RevenueModel(in string? clientId, in decimal value, in DateTime? paymentDate, in string? observation,
                in string? categoryId, in string costCenterId, in string? revenueId = null, in string? transactionId = null, in string? clientName = null)
            : base(transactionId ?? Guid.NewGuid().ToString(), value, paymentDate, observation, categoryId, costCenterId)
        {
            RevenueId = revenueId ?? Guid.NewGuid().ToString();
            ClientId = clientId;
            ClientName = clientName;
        }

        public RevenueModel()
        {
            
        }

        public string? RevenueId { get; set; }
        public string? ClientId { get; set; }
        public string? ClientName { get; set; }
    }
}
