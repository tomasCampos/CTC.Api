using CTC.Application.Shared.Data;
using CTC.Application.Shared.Models.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Data
{
    internal class DeleteSupplierRepository : IDeleteSupplierRepository
    {
        private readonly ISqlService _sqlService;

        public DeleteSupplierRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> DeleteSupplier(string supplierId, string personId)
        {
            var sqlCommands = BuildCommands(supplierId, personId);
            var result = await _sqlService.ExecuteWithTransactionAsync(sqlCommands);

            return result.Success;
        }

        public async Task<SupplierModel> GetSupplierById(string supplierId)
        {
            var result = await _sqlService.SelectAsync<SupplierModel>(SupplierSqlScripts.GET_SUPPLIER_BY_ID, new { supplier_id = supplierId });
            return result.FirstOrDefault();
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string supplierId, string personId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    SupplierSqlScripts.DELETE_SUPPLIER,
                    new
                    {
                        supplier_id = supplierId
                    }
                },

                {
                    PersonSqlScripts.DELETE_PERSON_SQL,
                    new
                    {
                        person_id = personId
                    }
                }
            };

            return commands;
        }

        #endregion
    }
}
