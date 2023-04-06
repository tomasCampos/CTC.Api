namespace CTC.Api.Controllers.CostCenter.Contracts
{
    public class RegisterCostCenterRequest
    {
        public string? Name { get; set; }
        public string? Observations { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? ExpectedClosingDate { get; set; }
        public DateTime? ClosingDate { get; set; }
        public string? ClientId { get; set; }
        public CostCenterAddress? Address { get; set; }
    }
}

