using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
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
            var result = await _sqlService.SelectSingleAsync<SupplierModel>(DeleteSupplierSqlScripts.GET_SUPPLIER_BY_ID_SQL_SCRIPT, new { supplier_id = supplierId });
            return result;
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(string supplierId, string personId)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    DeleteSupplierSqlScripts.DELETE_SUPPLIER_SQL,
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
