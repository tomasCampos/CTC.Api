namespace CTC.Application.Features.Supplier.UseCases.ListSuppliers.Data
{
    internal static class ListSuppliersSqlScripts
    {
        public static string ListSuppliersSelectStatement = @"SELECT
                                                            s.supplier_id AS SupplierId,
                                                                s.person_id AS personId,
                                                                p.person_document AS Document,
                                                                p.person_email AS Email,
                                                                p.person_phone AS Phone,
                                                                p.person_first_name AS FirstName";

        public static string ListSuppliersFromAndJoinsStatements = @"FROM
                                                            `heroku_3a06699194dd49a`.supplier s
                                                            INNER JOIN
                                                            `heroku_3a06699194dd49a`.person p ON s.person_id = p.person_id";

        public static string ListSuppliersWhereStatement = @"WHERE
                                                            (
                                                            p.person_first_name LIKE '%@search_param%'
                                                            OR p.person_document LIKE '%@search_param%'
                                                            OR p.person_email LIKE '%@search_param%'
                                                            OR p.person_phone LIKE '%@search_param%'
                                                            )";
    }
}
