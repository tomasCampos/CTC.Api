namespace CTC.Application.Features.User.UseCases.DeleteUser.Data
{
    internal class DeleteUserSqlScripts
    {
        public static string DELETE_USER_SQL = @"DELETE FROM `heroku_3a06699194dd49a`.user WHERE user_id = @user_id";

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
    }
}
