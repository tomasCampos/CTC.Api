namespace CTC.Application.Features.Category.UseCases.RegisterCategory.Data
{
    internal static class RegisterCategorySqlScripts
    {
        public static string COUNT_CATEGORY_BY_NAME_SQL = "SELECT COUNT(*) FROM heroku_3a06699194dd49a.Category WHERE category_name = @category_name";

        public static string INSERT_CATEGORY_SQL = @"INSERT INTO heroku_3a06699194dd49a.Category
                                                    (`category_id`,
                                                    `category_name`)
                                                    VALUES
                                                    (@category_id,
                                                    @category_name);";
    }
}
