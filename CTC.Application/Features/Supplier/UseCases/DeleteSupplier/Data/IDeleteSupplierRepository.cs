using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Data
{
    internal interface IDeleteSupplierRepository
    {
        Task<bool> DeleteSupplier(string supplierId, string personId);

        Task<SupplierModel> GetSupplierById(string supplierId);
    }
}
