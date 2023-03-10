namespace CTC.Application.Features.User.UseCases.UpdateUser.Data
{
    internal static class UpdateUserSqlScripts
    {
        public static string UPDATE_USER_SQL = @"UPDATE 
													`heroku_3a06699194dd49a`.`user`
												SET
													`user_last_name` = @user_last_name,
													`user_permission` = @user_permission,
													`user_password` = @user_password
												WHERE 
													`user_id` = @user_id;";

        public static string GET_USER_BY_ID_SQL_SCRIPT = @"SELECT
	                                            u.user_id AS userId,
                                                p.person_id AS personId,
                                                p.person_first_name AS firstName,
                                                p.person_email AS email,
                                                p.person_phone AS phone,
                                                u.user_last_name AS lastName,
                                                u.user_password AS password,
                                                u.user_permission AS permission,
                                                p.person_document AS document
                                            FROM heroku_3a06699194dd49a.User u
                                            INNER JOIN heroku_3a06699194dd49a.Person p on u.person_id = p.person_id
                                            WHERE u.user_id = @user_id";

        private static string GET_USER_BY_EMAIL_PHONE_OR_DOCUMENT = @"SELECT
                                                        u.user_id AS userId,
                                                        p.person_id AS personId,
                                                        p.person_first_name AS firstName,
                                                        p.person_email AS email,
                                                        p.person_phone AS phone,
                                                        u.user_last_name AS lastName,
                                                        u.user_password AS `password`,
                                                        u.user_permission AS permission,
                                                        p.person_document AS document
                                                    FROM 
                                                        heroku_3a06699194dd49a.User u
                                                    INNER JOIN 
                                                        heroku_3a06699194dd49a.Person p on u.person_id = p.person_id
                                                    @@WHERE_STATEMENT@@";

        public static string GetSelectUserQuery(string whereStatement)
        {
            return GET_USER_BY_EMAIL_PHONE_OR_DOCUMENT.Replace("@@WHERE_STATEMENT@@", whereStatement);
        }
    }
}
