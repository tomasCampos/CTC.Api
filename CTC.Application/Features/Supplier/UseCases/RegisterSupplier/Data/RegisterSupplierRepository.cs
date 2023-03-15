using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data
{
    internal sealed class RegisterSupplierRepository : IRegisterSupplierRepository
    {
        public Task<bool> InsertSupplier(SupplierModel model)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> VerifyIfSupplierAlreadyExists(string email, string phone, string document)
        {
            throw new System.NotImplementedException();
        }
    }
}
