using CTC.Application.Shared.Data;
using System.Linq;
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
            var result = await _sqlService.SelectAsync<SupplierModel>(SupplierSqlScripts.GET_SUPPLIER_BY_ID, new { supplier_id = supplierId });
            return result.FirstOrDefault();
        }
    }
}
