namespace CTC.Application.Features.User.UseCases.RegisterUser.Data
{
    internal static class RegisterUserSqlScripts
    {
        public static string INSERT_USER_SQL = @"INSERT INTO heroku_3a06699194dd49a.User
                                                (`user_id`,
                                                `user_last_name`,
                                                `user_permission`,
                                                `user_password`,
                                                `person_id`)
                                                VALUES
                                                (@user_id,
                                                @user_last_name,
                                                @user_permission,
                                                @user_password,
                                                @person_id);";

        public static string COUNT_USER_BY_EMAIL_PHONE_DOCUMENT = @"SELECT COUNT(*)
                                                                    FROM 
	                                                                    `heroku_3a06699194dd49a`.User u
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.Person p ON u.person_id = p.person_id
                                                                    WHERE 
	                                                                    p.person_email = @person_email
                                                                        OR p.person_phone = @person_phone
                                                                        OR p.person_document = @person_document";
    }
}
