using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data
{
    internal interface IRegisterSupplierRepository
    {
        Task<bool> InsertSupplier(SupplierModel model);
        Task<int> VerifyIfSupplierAlreadyExists(string email, string phone, string document);
    }
}
