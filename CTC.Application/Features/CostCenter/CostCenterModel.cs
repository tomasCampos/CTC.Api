using System;

namespace CTC.Application.Features.CostCenter
{
    internal sealed class CostCenterModel
    {
        public CostCenterModel(string id, string name, string observations, DateTime startingDate, DateTime expectedClosingDate, DateTime closingDate, string clientId, string addressId, string addresPostalCode, string addresStreetName, string addresNeighborhood, int addresNumber, string addresComplement, string addresCity, string addresState)
        {
            Id = id;
            Name = name;
            Observations = observations;
            StartingDate = startingDate;
            ExpectedClosingDate = expectedClosingDate;
            ClosingDate = closingDate;
            ClientId = clientId;
            AddressPostalCode = addresPostalCode;
            AddressStreetName = addresStreetName;
            AddressNeighborhood = addresNeighborhood;
            AddressNumber = addresNumber;
            AddressComplement = addresComplement;
            AddressCity = addresCity;
            AddressState = addresState;
        }

        public CostCenterModel(string name, string observations, DateTime startingDate, DateTime expectedClosingDate, DateTime closingDate, string clientId, string addresPostalCode, string addresStreetName, string addresNeighborhood, int addresNumber, string addresComplement, string addresCity, string addresState)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Observations = observations;
            StartingDate = startingDate;
            ExpectedClosingDate = expectedClosingDate;
            ClosingDate = closingDate;
            ClientId = clientId;
            AddressPostalCode = addresPostalCode;
            AddressStreetName = addresStreetName;
            AddressNeighborhood = addresNeighborhood;
            AddressNumber = addresNumber;
            AddressComplement = addresComplement;
            AddressCity = addresCity;
            AddressState = addresState;
        }

        public CostCenterModel()
        {

        }

        public string Id { get; }
        public string Name { get; set; }
        public string Observations { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime ExpectedClosingDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public string ClientId { get; set; }
        public string AddressPostalCode { get; set; }
        public string AddressStreetName { get; set; }
        public string AddressNeighborhood { get; set; }
        public int AddressNumber { get; set; }
        public string AddressComplement { get; set; }
        public string AddressCity { get; set; }
        public string AddressState { get; set; }
    }
}
