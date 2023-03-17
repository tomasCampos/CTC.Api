using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.ListSuppliers.Data
{
    internal interface IListSuppliersRepository
    {
        Task<PaginatedQueryResult<SupplierModel>> ListSuppliers(QueryRequest queryParams);
    }
}
