using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Analytics.UseCases.GetOverview.UseCase
{
    public sealed class GetOverviewInput : IInput
    {
        public int Year { get; set; }
    }
}
