using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.CostCenter.UseCases.ListCostCenter.UseCase
{
    public sealed class ListCostCentersInput : QueryInput
    {
        public ListCostCentersInput(QueryRequest request) : base(request) {}
    }
}
