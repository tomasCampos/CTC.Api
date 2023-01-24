namespace CTC.Application.Features.User.UseCases.GetUser.Data
{
    internal static class GetUserSqlScripts
    {
        public static string GetUserByEmail = @"SELECT
	                                            u.user_id AS UserId,
                                                p.person_id AS PersonId,
                                                p.person_first_name AS FirstName,
                                                p.person_email AS Email,
                                                p.person_phone AS Phone,
                                                u.user_last_name AS LastName,
                                                u.user_password AS `Password`,
                                                u.user_permission AS Permission,
                                                p.person_document AS Document
                                            FROM railway.User u
                                            INNER JOIN railway.Person p on u.person_id = p.person_id
                                            WHERE p.person_email = @person_email";
    }
}
