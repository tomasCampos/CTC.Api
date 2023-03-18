namespace CTC.Application.Features.Client.UseCases.RegisterClient.Data
{
    internal static class RegisterClientSqlScripts
    {
        public static string INSERT_CLIENT_SQL = @"INSERT INTO `heroku_3a06699194dd49a`.`Client`
                                                                    (`client_id`,
                                                                    `person_id`)
                                                                    VALUES
                                                                    (@client_id,
                                                                    @person_id);";

        public static string COUNT_CLIENT_BY_EMAIL_PHONE_DOCUMENT = @"SELECT COUNT(*)
                                                                    FROM 
	                                                                    `heroku_3a06699194dd49a`.Client c
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.Person p ON c.person_id = p.person_id
                                                                    WHERE 
	                                                                    p.person_email = @person_email
                                                                        OR p.person_phone = @person_phone
                                                                        OR p.person_document = @person_document";
    }
}
