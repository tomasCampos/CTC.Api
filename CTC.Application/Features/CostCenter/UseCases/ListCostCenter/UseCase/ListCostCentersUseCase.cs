using CTC.Application.Features.CostCenter.UseCases.ListCostCenter.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.ListCostCenter.UseCase
{
    internal sealed class ListCostCentersUseCase : IUseCase<ListCostCentersInput, Output>
    {
        private readonly IListCostCentersRepository _repository;

        public ListCostCentersUseCase(IListCostCentersRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(ListCostCentersInput input)
        {
            var result = await _repository.ListCostCenters(input.Request);
            return Output.CreateOkResult(result);
        }
    }
}
