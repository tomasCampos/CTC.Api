using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
using System;

namespace CTC.Application.Features.CostCenter
{
    internal sealed class CostCenterModel
    {
        public CostCenterModel(in string name, in string? observations, in DateTime startingDate, in DateTime? expectedClosingDate, in DateTime? closingDate, in string clientId,
            in string? addresPostalCode, in string? addresStreetName, in string? addresNeighborhood, in int? addresNumber, in string? addresComplement, in string? addresCity,
            in string? addresState, in string? id = null)
        {
            Id = id ?? Guid.NewGuid().ToString();
            Name = name;
            Observations = observations;
            StartingDate = startingDate;
            ExpectedClosingDate = expectedClosingDate;
            ClosingDate = closingDate;
            ClientId = clientId;
            AddressPostalCode = addresPostalCode ?? string.Empty;
            AddressStreetName = addresStreetName ?? string.Empty;
            AddressNeighborhood = addresNeighborhood ?? string.Empty;
            AddressNumber = addresNumber;
            AddressComplement = addresComplement ?? string.Empty;
            AddressCity = addresCity ?? string.Empty;
            AddressState = addresState ?? string.Empty;
        }

        public CostCenterModel() {}

        public string Id { get; }
        public string Name { get; set; }
        public string? Observations { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime? ExpectedClosingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string ClientId { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressNeighborhood { get; set; }
        public int? AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
    }
}
