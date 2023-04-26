using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Revenue.UseCases.GetRevenue.UseCase
{
    public sealed class GetRevenueInput : IInput
    {
        public string? RevenueId { get; set; }
    }
}
