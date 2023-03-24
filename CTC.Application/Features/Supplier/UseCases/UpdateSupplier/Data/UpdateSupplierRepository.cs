using CTC.Application.Shared.Data;
using CTC.Application.Shared.Person;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.UpdateSupplier.Data
{
    internal sealed class UpdateSupplierRepository : IUpdateSupplierRepository
    {
        private readonly ISqlService _sqlService;

        public UpdateSupplierRepository(ISqlService sqlService)
        {
            _sqlService = sqlService;
        }

        public async Task<SupplierModel> GetSupplierById(string id)
        {
            var supplier = await _sqlService.SelectAsync<SupplierModel>(SupplierSqlScripts.GET_SUPPLIER_BY_ID, new { supplier_id = id });
            return supplier.FirstOrDefault();
        }

        public async Task<List<SupplierModel>> GetSuppliersByDocument(string document)
        {
            var sql = SupplierSqlScripts.GetSelectSupplierQuery("WHERE p.person_document = @person_document");
            var result = await _sqlService.SelectAsync<SupplierModel>(sql, new { person_document = document });
            return result.ToList();
        }

        public async Task<List<SupplierModel>> GetSuppliersByEmail(string email)
        {
            var sql = SupplierSqlScripts.GetSelectSupplierQuery("WHERE p.person_email = @person_email");
            var result = await _sqlService.SelectAsync<SupplierModel>(sql, new { person_email = email });
            return result.ToList();
        }

        public async Task<List<SupplierModel>> GetSuppliersByPhone(string phone)
        {
            var sql = SupplierSqlScripts.GetSelectSupplierQuery("WHERE p.person_phone = @person_phone");
            var result = await _sqlService.SelectAsync<SupplierModel>(sql, new { person_phone = phone });
            return result.ToList();
        }

        public async Task<int> UpdateSupplier(SupplierModel model)
        {
            var updatePersonSqlCommand = PersonSqlScripts.GetUpdatePersonSql(model);

            var result = await _sqlService.ExecuteAsync(updatePersonSqlCommand, new
            {
                person_id = model.PersonId,
                person_first_name = model.FirstName,
                person_email = model.Email,
                person_phone = model.Phone,
                person_document = model.Document
            });

            return result;
        }
    }
}
