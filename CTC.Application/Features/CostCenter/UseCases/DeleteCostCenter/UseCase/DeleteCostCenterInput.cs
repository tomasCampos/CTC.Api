using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.UseCase
{
    public sealed class DeleteCostCenterInput : IInput
    {
        public DeleteCostCenterInput(string costCenterId)
        {
            CostCenterId = costCenterId;
        }

        public string CostCenterId { get; set; }
    }
}
