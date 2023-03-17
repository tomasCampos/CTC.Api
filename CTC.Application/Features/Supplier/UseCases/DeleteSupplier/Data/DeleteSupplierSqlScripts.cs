namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Data
{
    internal class DeleteSupplierSqlScripts
    {
        public static string DELETE_SUPPLIER_SQL = @"DELETE FROM `heroku_3a06699194dd49a`.supplier WHERE supplier_id = @supplier_id";

        public static string GET_SUPPLIER_BY_ID_SQL_SCRIPT = @"SELECT
                                                            s.supplier_id AS SupplierId,
                                                            s.person_id AS PersonId,
                                                            p.person_document AS Document,
                                                            p.person_email AS Email,
                                                            p.person_phone AS Phone,
                                                            p.person_first_name AS FirstName
                                                        FROM
                                                            `heroku_3a06699194dd49a`.supplier s
                                                        INNER JOIN
                                                            `heroku_3a06699194dd49a`.person p ON s.person_id = p.person_id
                                                        WHERE
                                                            s.supplier_id = @supplier_id";
    }
}
