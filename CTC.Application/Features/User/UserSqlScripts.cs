namespace CTC.Application.Features.User
{
    internal static class UserSqlScripts
    {
        #region SELECT

        public static string GET_USER_BY_EMAIL = @"SELECT
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
                                                WHERE 
                                                    p.person_email = @person_email";

        public static string GET_USER_BY_ID = @"SELECT
	                                                u.user_id AS userId,
                                                    p.person_id AS personId,
                                                    p.person_first_name AS firstName,
                                                    p.person_email AS email,
                                                    p.person_phone AS phone,
                                                    u.user_last_name AS lastName,
                                                    u.user_password AS password,
                                                    u.user_permission AS permission,
                                                    p.person_document AS document
                                                FROM 
                                                    heroku_3a06699194dd49a.User u
                                                INNER JOIN 
                                                    heroku_3a06699194dd49a.Person p on u.person_id = p.person_id
                                                WHERE 
                                                    u.user_id = @user_id";

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

        public static string COUNT_USER_BY_EMAIL_PHONE_DOCUMENT = @"SELECT COUNT(*)
                                                                    FROM 
	                                                                    `heroku_3a06699194dd49a`.User u
                                                                    INNER JOIN 
	                                                                    `heroku_3a06699194dd49a`.Person p ON u.person_id = p.person_id
                                                                    WHERE 
	                                                                    p.person_email = @person_email
                                                                        OR p.person_phone = @person_phone
                                                                        OR p.person_document = @person_document";

        public static string LIST_USERS_SELECT_STATEMENT = @"SELECT
											u.user_id AS userId,
											p.person_id AS personId,
											p.person_first_name AS firstName,
											p.person_email AS email,
											p.person_phone AS phone,
											u.user_last_name AS lastName,
											u.user_password AS `password`,
											u.user_permission AS permission,
											p.person_document AS document";

        public static string LIST_USERS_FROM_AND_JOIN_STATEMENT = @"FROM 
																	heroku_3a06699194dd49a.User u
																INNER JOIN 
																	heroku_3a06699194dd49a.Person p on u.person_id = p.person_id";

        public static string LIST_USERS_WHERE_STATEMENT = @"WHERE 
														(p.person_first_name LIKE '%@search_param%'
														OR u.user_last_name LIKE '%@search_param%'
														OR p.person_email LIKE '%@search_param%')";

        public static string GetSelectUserQuery(string whereStatement)
        {
            return GET_USER_BY_EMAIL_PHONE_OR_DOCUMENT.Replace("@@WHERE_STATEMENT@@", whereStatement);
        }

        #endregion

        #region INSERT 

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

        #endregion

        #region UPDATE

        public static string UPDATE_USER_SQL = @"UPDATE 
													`heroku_3a06699194dd49a`.`user`
												SET
													`user_last_name` = @user_last_name,
													`user_permission` = @user_permission,
													`user_password` = @user_password
												WHERE 
													`user_id` = @user_id;";

        #endregion

        #region DELETE

        public static string DELETE_USER = @"DELETE FROM `heroku_3a06699194dd49a`.user WHERE user_id = @user_id";

        #endregion
    }
}
