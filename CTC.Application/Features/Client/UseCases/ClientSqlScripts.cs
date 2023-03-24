namespace CTC.Application.Features.Client.UseCases
{
    internal static class ClientSqlScripts
    {
        #region SELECT

        public static string GET_CLIENT_BY_ID = @"SELECT
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

        public static string LIST_CLIENTS_SELECT_STATEMENT = @"SELECT
                                                                s.client_id AS ClientId,
                                                                s.person_id AS personId,
                                                                p.person_document AS Document,
                                                                p.person_email AS Email,
                                                                p.person_phone AS Phone,
                                                                p.person_first_name AS FirstName";

        public static string LIST_CLIENTS_FROM_AND_JOIN_STATEMENTS = @"FROM
                                                                    `heroku_3a06699194dd49a`.client s
                                                                INNER JOIN
                                                                    `heroku_3a06699194dd49a`.person p ON s.person_id = p.person_id";

        public static string LIST_CLIENT_WHERE_STATEMENT = @"WHERE
                                                            (
                                                            p.person_first_name LIKE '%@search_param%'
                                                            OR p.person_document LIKE '%@search_param%'
                                                            OR p.person_email LIKE '%@search_param%'
                                                            OR p.person_phone LIKE '%@search_param%'
                                                            )";

        public static string COUNT_CLIENT_BY_EMAIL_PHONE_DOCUMENT = @"SELECT COUNT(*)
                                                                    FROM 
	                                                                    `heroku_3a06699194dd49a`.Client c
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.Person p ON c.person_id = p.person_id
                                                                    WHERE 
	                                                                    p.person_email = @person_email
                                                                        OR p.person_phone = @person_phone
                                                                        OR p.person_document = @person_document";

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

        #endregion

        #region INSERT

        public static string INSERT_CLIENT = @"INSERT INTO `heroku_3a06699194dd49a`.`Client`
                                                                    (`client_id`,
                                                                    `person_id`)
                                                                    VALUES
                                                                    (@client_id,
                                                                    @person_id);";

        #endregion

        #region DELETE

        public static string DELETE_CLIENT = @"DELETE FROM `heroku_3a06699194dd49a`.client WHERE client_id = @client_id";

        #endregion
    }
}
