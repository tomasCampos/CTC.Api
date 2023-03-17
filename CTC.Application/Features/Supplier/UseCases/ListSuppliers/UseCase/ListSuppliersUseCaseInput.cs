using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Supplier.UseCases.ListSuppliers.UseCase
{
    public sealed class ListSuppliersUseCaseInput : QueryInput
    {
        public ListSuppliersUseCaseInput(QueryRequest request) : base(request) {}
    }
}
