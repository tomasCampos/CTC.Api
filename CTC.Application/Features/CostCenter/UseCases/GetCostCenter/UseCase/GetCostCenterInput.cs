using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.CostCenter.UseCases.GetCostCenter.UseCase
{
    public sealed class GetCostCenterInput : IInput
    {
        public GetCostCenterInput(string id)
        {
            Id = id;
        }

        public string Id { get; set; }
    }
}
