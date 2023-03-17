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
            var result = await _sqlService.SelectPaginated<SupplierModel>(queryParams, ListSuppliersSqlScripts.ListSuppliersSelectStatement, ListSuppliersSqlScripts.ListSuppliersFromAndJoinsStatements,
                                    ListSuppliersSqlScripts.ListSuppliersWhereStatement);

            return result;
        }
    }
}
