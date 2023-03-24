using CTC.Application.Shared.Data;
using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.ListSuppliers.Data
{
    internal sealed class ListSuppliersRepository : IListSuppliersRepository
    {
        private readonly ISqlService _sqlService;

        public ListSuppliersRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<PaginatedQueryResult<SupplierModel>> ListSuppliers(QueryRequest queryParams)
        {
            var result = await _sqlService.SelectPaginated<SupplierModel>(queryParams, SupplierSqlScripts.LIST_SUPPLIERS_SELECT_STATEMENT, SupplierSqlScripts.LIST_SUPPLIERS_FROM_AND_JOIN_STATEMENT,
                                    SupplierSqlScripts.LIST_SUPPLIERS_WHERE_STATEMENT);

            return result;
        }
    }
}
