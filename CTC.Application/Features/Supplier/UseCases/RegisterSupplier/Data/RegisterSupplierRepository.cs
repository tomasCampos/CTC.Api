using CTC.Application.Features.User.UseCases.RegisterUser.Data;
using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data
{
    internal sealed class RegisterSupplierRepository : IRegisterSupplierRepository
    {
        private readonly ISqlService _sqlService;

        public RegisterSupplierRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<bool> InsertSupplier(SupplierModel model)
        {
            var commands = BuildCommands(model);

            var result = await _sqlService.ExecuteWithTransactionAsync(commands);
            return result.Success;
        }

        public async Task<int> VerifyIfSupplierAlreadyExists(string email, string phone, string document)
        {
            return await _sqlService.CountAsync(RegisterSupplierSqlScripts.COUNT_SUPPLIER_BY_EMAIL_PHONE_DOCUMENT, new { person_email = email, person_phone = phone, person_document = document });
        }

        #region PrivateMethods

        private static Dictionary<string, object?> BuildCommands(SupplierModel model)
        {
            var commands = new Dictionary<string, object?>
            {
                {
                    PersonSqlScripts.INSERT_PERSON_SQL,
                    new
                    {
                        person_id = model.PersonId,
                        person_first_name = model.FirstName,
                        person_email = model.Email,
                        person_phone = model.Phone,
                        person_document = model.Document
                    }
                },

                {
                    RegisterSupplierSqlScripts.INSERT_SUPPLIER_SQL,
                    new
                    {
                        supplier_id = model.SupplierId,
                        person_id = model.PersonId
                    }
                }
            };
            return commands;
        }

        #endregion
    }
}
