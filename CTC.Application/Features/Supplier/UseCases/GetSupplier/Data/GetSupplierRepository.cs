using CTC.Application.Shared.Data;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.GetSupplier.Data
{
    internal sealed class GetSupplierRepository : IGetSupplierRepository
    {
        private readonly ISqlService _sqlService;

        public GetSupplierRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<SupplierModel> GetSupplierById(string supplierId)
        {
            var result = await _sqlService.SelectSingleAsync<SupplierModel>(GetSupplierSqlScripts.GET_SUPPLIER_BY_ID_SQL_SCRIPT, new { supplier_id = supplierId });
            return result;
        }
    }
}
