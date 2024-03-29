﻿using System;

namespace CTC.Application.Shared.Models.Transaction
{
    internal abstract class TransactionModel
    {
        protected TransactionModel() { }

        protected TransactionModel(in string transactionId, in decimal value, in DateTime? paymentDate, in string? 
            observation, in string? categoryId, in string costCenterId, in string? categoryName = null, in string? costCenterName = null)
        {
            TransactionId = transactionId;
            Value = value;
            PaymentDate = paymentDate;
            Observation = observation;
            CategoryId = categoryId;
            CostCenterId = costCenterId;
            CategoryName = categoryName;
            CostCenterName = costCenterName;
        }

        public string? TransactionId { get; set; }
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Observation { get; set; }
        public string? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? CostCenterId { get; set; }
        public string? CostCenterName { get; set; }
    }
}
