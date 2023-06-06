using System.Net;

namespace CTC.Integration.Test.Features.CostCenter.Dtos
{
    internal sealed class GetCostCenterDto
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Observations { get; set; }
        public string? StartingDate { get; set; }
        public string? ExpectedClosingDate { get; set; }
        public string? ClosingDate { get; set; }
        public AddressDto? Address { get; set; }
        public ClientDto? Client { get; set; }
    }
}
