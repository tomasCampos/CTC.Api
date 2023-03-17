using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Supplier.UseCases.GetSupplier.UseCase
{
    public class GetSupplierInput : IInput
    {
        public string SupplierId { get; set; }
    }
}
