namespace CTC.Application.Features.User.UseCases.GetUser.Data
{
    internal static class GetUserSqlScripts
    {
        public static string GetUserByEmail = @"SELECT
	                                            u.user_id AS userId,
                                                p.person_id AS personId,
                                                p.person_first_name AS firstName,
                                                p.person_email AS email,
                                                p.person_phone AS phone,
                                                u.user_last_name AS lastName,
                                                u.user_password AS `password`,
                                                u.user_permission AS permission,
                                                p.person_document AS document
                                            FROM heroku_3a06699194dd49a.User u
                                            INNER JOIN heroku_3a06699194dd49a.Person p on u.person_id = p.person_id
                                            WHERE p.person_email = @person_email";
    }
}
