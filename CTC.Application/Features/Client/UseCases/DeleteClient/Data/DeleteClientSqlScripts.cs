﻿namespace CTC.Application.Features.Client.UseCases.DeleteClient.Data
{
    internal class DeleteClientSqlScripts
    {
        public static string DELETE_CLIENT_SQL = @"DELETE FROM `heroku_3a06699194dd49a`.client WHERE client_id = @client_id";

        public static string GET_CLIENT_BY_ID_SQL_SCRIPT = @"SELECT
                                                            s.client_id AS ClientId,
                                                            s.person_id AS PersonId,
                                                            p.person_document AS Document,
                                                            p.person_email AS Email,
                                                            p.person_phone AS Phone,
                                                            p.person_first_name AS FirstName
                                                        FROM
                                                            `heroku_3a06699194dd49a`.client s
                                                        INNER JOIN
                                                            `heroku_3a06699194dd49a`.person p ON s.person_id = p.person_id
                                                        WHERE
                                                            s.client_id = @client_id";
    }
}