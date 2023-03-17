using CTC.Application.Features.Supplier.UseCases.ListSuppliers.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.ListSuppliers.UseCase
{
    internal sealed class ListSuppliersUseCase : IUseCase<ListSuppliersInput, Output>
    {
        private readonly IListSuppliersRepository _suppliersRepository;

        public ListSuppliersUseCase(IUseCaseAuthorizationService useCaseAuthorizationService, IListSuppliersRepository suppliersRepository)
        {
            _suppliersRepository = suppliersRepository;
        }
        
        public async Task<Output> Execute(ListSuppliersInput input)
        {
            var result = await _suppliersRepository.ListSuppliers(input.Request);
            return Output.CreateOkResult(result);
        }
    }
}
