using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Revenue.UseCases.DeleteRevenue.UseCase
{
    public sealed class DeleteRevenueInput : IInput
    {
        public string? RevenueId { get; set; }
    }
}
