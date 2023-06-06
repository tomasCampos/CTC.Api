namespace CTC.Integration.Test.Features.CostCenter.Dtos
{
    internal sealed class ListCostCenterDto
    {
        public int TotalCount { get; set; }

        public List<GetCostCenterDto>? Results { get; set; }
    }
}
