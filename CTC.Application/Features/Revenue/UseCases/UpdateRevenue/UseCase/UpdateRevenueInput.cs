using CTC.Application.Shared.UseCase.IO;
using System;

namespace CTC.Application.Features.Revenue.UseCases.UpdateRevenue.UseCase
{
    public sealed class UpdateRevenueInput : IInput
    {
        public string? RevenueId { get; set; }
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Observation { get; set; }
        public string? CategoryId { get; set; }
        public string? CostCenterId { get; set; }
        public string? ClientId { get; set; }
    }
}
