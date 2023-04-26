using CTC.Application.Features.Revenue.UseCases.ListRevenues.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.ListRevenues.UseCase
{
    internal class ListRevenuesUseCase : IUseCase<ListRevenuesInput, Output>
    {
        private readonly IListRevenuesRepository _repository;

        public ListRevenuesUseCase(IListRevenuesRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(ListRevenuesInput input)
        {
            var result = await _repository.ListRevenues(input.Request, input.CostCenterName, input.CategoryName, input.Year);
            return Output.CreateOkResult(result);
        }
    }
}
