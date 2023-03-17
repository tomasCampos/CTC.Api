namespace CTC.Application.Features.Supplier.UseCases.UpdateSupplier.Data
{
    internal static class UpdateSupplierSqlScripts
    {
        public static string GET_SUPPLIER_BY_ID_SQL = @"SELECT
                                                            s.supplier_id AS SupplierId,
                                                            s.person_id AS personId,
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

        private static string GET_SUPPLIER_BY_EMAIL_PHONE_OR_DOCUMENT = @"SELECT
	                                                                        s.supplier_id AS supplierId,
	                                                                        p.person_id AS personId,
	                                                                        p.person_first_name AS firstName,
	                                                                        p.person_email AS email,
	                                                                        p.person_phone AS phone
                                                                        FROM 
	                                                                        `heroku_3a06699194dd49a`.Supplier s
                                                                        INNER JOIN 
	                                                                        `heroku_3a06699194dd49a`.Person p on s.person_id = p.person_id
                                                                        @@WHERE_STATEMENT@@";

        public static string GetSelectSupplierQuery(string whereStatement)
        {
            return GET_SUPPLIER_BY_EMAIL_PHONE_OR_DOCUMENT.Replace("@@WHERE_STATEMENT@@", whereStatement);
        }
    }
}
