using CTC.Application.Shared.UseCase.IO;
using System;

namespace CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase
{
    public sealed class RegisterCostCenterInput : IInput
    {
        public string? Name { get; set; }
        public string? Observations { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? ExpectedClosingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? ClientId { get; set; }
        public string? AddressPostalCode { get; set; }
        public string? AddressStreetName { get; set; }
        public string? AddressNeighborhood { get; set; }
        public string? AddressNumber { get; set; }
        public string? AddressComplement { get; set; }
        public string? AddressCity { get; set; }
        public string? AddressState { get; set; }
    }
}
