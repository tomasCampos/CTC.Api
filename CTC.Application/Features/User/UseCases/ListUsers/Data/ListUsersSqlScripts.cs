namespace CTC.Application.Features.User.UseCases.ListUsers.Data
{
    internal static class ListUsersSqlScripts
    {
		public static string ListUsersSelectStatement = @"SELECT
											u.user_id AS userId,
											p.person_id AS personId,
											p.person_first_name AS firstName,
											p.person_email AS email,
											p.person_phone AS phone,
											u.user_last_name AS lastName,
											u.user_password AS `password`,
											u.user_permission AS permission,
											p.person_document AS document";

		public static string ListUsersFromAndJoinsStatements = @"FROM 
																	heroku_3a06699194dd49a.User u
																INNER JOIN 
																	heroku_3a06699194dd49a.Person p on u.person_id = p.person_id";

		public static string ListUsersWhereStatement = @"WHERE 
														(p.person_first_name LIKE '%@search_param%'
														OR u.user_last_name LIKE '%@search_param%'
														OR p.person_email LIKE '%@search_param%')";
    }
}
