using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.UpdateSupplier.Data
{
    internal interface IUpdateSupplierRepository
    {
        Task<int> UpdateSupplier(SupplierModel model);

        Task<SupplierModel> GetSupplierById(string id);

        Task<List<SupplierModel>> GetSuppliersByEmail(string email);

        Task<List<SupplierModel>> GetSuppliersByDocument(string document);

        Task<List<SupplierModel>> GetSuppliersByPhone(string phone);
    }
}