namespace CTC.Application.Features.Client.UseCases.ListClients.Data
{
    internal static class ListClientsSqlScripts
    {
        public static string ListClientsSelectStatement = @"SELECT
                                                                s.client_id AS ClientId,
                                                                s.person_id AS personId,
                                                                p.person_document AS Document,
                                                                p.person_email AS Email,
                                                                p.person_phone AS Phone,
                                                                p.person_first_name AS FirstName";

        public static string ListClientsFromAndJoinsStatements = @"FROM
                                                                    `heroku_3a06699194dd49a`.client s
                                                                INNER JOIN
                                                                    `heroku_3a06699194dd49a`.person p ON s.person_id = p.person_id";

        public static string ListClientsWhereStatement = @"WHERE
                                                            (
                                                            p.person_first_name LIKE '%@search_param%'
                                                            OR p.person_document LIKE '%@search_param%'
                                                            OR p.person_email LIKE '%@search_param%'
                                                            OR p.person_phone LIKE '%@search_param%'
                                                            )";
    }
}
