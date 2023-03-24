namespace CTC.Application.Features.Supplier
{
    internal static class SupplierSqlScripts
    {
        #region SELECT

        public static string GET_SUPPLIER_BY_ID = @"SELECT
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

        public static string LIST_SUPPLIERS_SELECT_STATEMENT = @"SELECT
                                                            s.supplier_id AS SupplierId,
                                                                s.person_id AS personId,
                                                                p.person_document AS Document,
                                                                p.person_email AS Email,
                                                                p.person_phone AS Phone,
                                                                p.person_first_name AS FirstName";

        public static string LIST_SUPPLIERS_FROM_AND_JOIN_STATEMENT = @"FROM
                                                                        `heroku_3a06699194dd49a`.supplier s
                                                                        INNER JOIN
                                                                        `heroku_3a06699194dd49a`.person p ON s.person_id = p.person_id";

        public static string LIST_SUPPLIERS_WHERE_STATEMENT = @"WHERE
                                                            (
                                                            p.person_first_name LIKE '%@search_param%'
                                                            OR p.person_document LIKE '%@search_param%'
                                                            OR p.person_email LIKE '%@search_param%'
                                                            OR p.person_phone LIKE '%@search_param%'
                                                            )";

        public static string COUNT_SUPPLIER_BY_EMAIL_PHONE_DOCUMENT = @"SELECT COUNT(*)
                                                                    FROM 
	                                                                    `heroku_3a06699194dd49a`.Supplier s
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.Person p ON s.person_id = p.person_id
                                                                    WHERE 
	                                                                    p.person_email = @person_email
                                                                        OR p.person_phone = @person_phone
                                                                        OR p.person_document = @person_document";

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

        #endregion

        #region INSERT

        public static string INSERT_SUPPLIER = @"INSERT INTO `heroku_3a06699194dd49a`.`supplier`
                                                                    (`supplier_id`,
                                                                    `person_id`)
                                                                    VALUES
                                                                    (@supplier_id,
                                                                    @person_id);";

        #endregion

        #region DELETE

        public static string DELETE_SUPPLIER = @"DELETE FROM `heroku_3a06699194dd49a`.supplier WHERE supplier_id = @supplier_id";

        #endregion
    }
}
