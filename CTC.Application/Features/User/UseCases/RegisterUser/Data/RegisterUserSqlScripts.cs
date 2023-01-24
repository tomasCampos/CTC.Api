namespace CTC.Application.Features.User.UseCases.RegisterUser.Data
{
    internal static class RegisterUserSqlScripts
    {
        public static string INSERT_USER_SQL = @"INSERT INTO railway.User
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

        public static string COUNT_USER_BY_EMAIL = @"SELECT COUNT(*)
                                                    FROM railway.Person p
                                                    WHERE p.person_email = @person_email";
    }
}
