using CTC.Application.Features.Supplier.UseCases.GetSupplier.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.GetSupplier.UseCase
{
    internal sealed class GetSupplierUseCase : IUseCase<GetSupplierInput, Output>
    {
        private readonly IGetSupplierRepository _repository;

        public GetSupplierUseCase(IGetSupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(GetSupplierInput input)
        {
            var result = await _repository.GetSupplierById(input.SupplierId);
            return Output.CreateOkResult(result);
        }
    }
}
