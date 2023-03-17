using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.GetSupplier.Data
{
    internal interface IGetSupplierRepository
    {
        public Task<SupplierModel> GetSupplierById(string supplierId);
    }
}
