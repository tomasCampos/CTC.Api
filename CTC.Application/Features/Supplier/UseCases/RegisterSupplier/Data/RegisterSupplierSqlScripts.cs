namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data
{
    internal static class RegisterSupplierSqlScripts
    {
        public static string INSERT_SUPPLIER_SQL = @"INSERT INTO `heroku_3a06699194dd49a`.`supplier`
                                                                    (`supplier_id`,
                                                                    `person_id`)
                                                                    VALUES
                                                                    (@supplier_id,
                                                                    @person_id);";

        public static string COUNT_SUPPLIER_BY_EMAIL_PHONE_DOCUMENT = @"SELECT COUNT(*)
                                                                    FROM 
	                                                                    `heroku_3a06699194dd49a`.Supplier s
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.Person p ON s.person_id = p.person_id
                                                                    WHERE 
	                                                                    p.person_email = @person_email
                                                                        OR p.person_phone = @person_phone
                                                                        OR p.person_document = @person_document";
    }
}
