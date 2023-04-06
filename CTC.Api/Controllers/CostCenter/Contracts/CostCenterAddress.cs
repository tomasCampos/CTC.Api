namespace CTC.Api.Controllers.CostCenter.Contracts
{
    public class CostCenterAddress
    {
        public string? PostalCode { get; set; }
        public string? StreetName { get; set; }
        public string? Neighborhood { get; set; }
        public int? Number { get; set; }
        public string? Complement { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
    }
}
