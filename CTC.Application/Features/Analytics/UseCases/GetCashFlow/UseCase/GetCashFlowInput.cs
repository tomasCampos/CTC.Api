using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Analytics.UseCases.GetCashFlow.UseCase
{
    public sealed class GetCashFlowInput : IInput
    {
        public int Year { get; set; }
    }
}
