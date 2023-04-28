using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Analytics.UseCases.GetCostCenterSummary.UseCase
{
    public sealed class GetCostCenterSummaryInput : IInput
    {
        public int Year { get; set; }
    }
}
