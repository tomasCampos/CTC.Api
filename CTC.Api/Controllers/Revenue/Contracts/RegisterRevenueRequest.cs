namespace CTC.Api.Controllers.Revenue.Contracts
{
    public sealed class RegisterRevenueRequest
    {
        public decimal? Value { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? Observation { get; set; }
        public string? CategoryId { get; set; }
        public string? CostCenterId { get; set; }
    }
}
