namespace CTC.Application.Features.Client.UseCases.GetClient.Data
{
    internal static class GetClientSqlScripts
    {
        public static string GET_CLIENT_BY_ID_SQL_SCRIPT = @"SELECT
                                                            c.client_id AS ClientId,
                                                            c.person_id AS PersonId,
                                                            p.person_document AS Document,
                                                            p.person_email AS Email,
                                                            p.person_phone AS Phone,
                                                            p.person_first_name AS FirstName
                                                        FROM
                                                            `heroku_3a06699194dd49a`.client c
                                                        INNER JOIN
                                                            `heroku_3a06699194dd49a`.person p ON c.person_id = p.person_id
                                                        WHERE
                                                            c.client_id = @client_id";
    }
}
