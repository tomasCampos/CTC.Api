using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier.UseCase
{
    public sealed class DeleteSupplierInput : IInput
    {
        public string? SupplierId { get; set; }
    }
}
