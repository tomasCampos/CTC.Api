namespace CTC.Application.Features.Client.UseCases.UpdateClient.Data
{
    internal sealed class UpdateClientSqlScripts
    {
        public static string GET_CLIENT_BY_ID_SQL = @"SELECT
                                                            s.client_id AS clientId,
                                                            s.person_id AS personId,
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

        private static string GET_CLIENT_BY_EMAIL_PHONE_OR_DOCUMENT = @"SELECT
	                                                                        s.client_id AS clientId,
	                                                                        p.person_id AS personId,
	                                                                        p.person_first_name AS firstName,
	                                                                        p.person_email AS email,
	                                                                        p.person_phone AS phone
                                                                        FROM 
	                                                                        `heroku_3a06699194dd49a`.Client s
                                                                        INNER JOIN 
	                                                                        `heroku_3a06699194dd49a`.Person p on s.person_id = p.person_id
                                                                        @@WHERE_STATEMENT@@";

        public static string GetSelectClientQuery(string whereStatement)
        {
            return GET_CLIENT_BY_EMAIL_PHONE_OR_DOCUMENT.Replace("@@WHERE_STATEMENT@@", whereStatement);
        }
    }
}
