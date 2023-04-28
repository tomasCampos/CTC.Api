using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenterReport.UseCase
{
    public sealed class GetCostCenterReportInput : IInput
    {
        public string? CostCenterId { get; set; }
    }
}
