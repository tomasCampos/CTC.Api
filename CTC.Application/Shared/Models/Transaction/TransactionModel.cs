using System;

namespace CTC.Application.Shared.Models.Transaction
{
    internal abstract class TransactionModel
    {
        protected TransactionModel() { }

        protected TransactionModel(in string id, in decimal value, in DateTime paymentDate, in string observation, in string categoryId, in string costCenterId)
        {
            Id = id;
            Value = value;
            PaymentDate = paymentDate;
            Observation = observation;
            CategoryId = categoryId;
            CostCenterId = costCenterId;
        }

        public string? Id { get; set; }
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Observation { get; set; }
        public string? CategoryId { get; set; }
        public string? CostCenterId { get; set; }
    }
}
