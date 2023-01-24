namespace CTC.Application.Features.Category.UseCases.RegisterCategory.Data
{
    internal static class RegisterCategorySqlScripts
    {
        public static string COUNT_CATEGORY_BY_NAME_SQL = "SELECT COUNT(*) FROM Category WHERE category_name = @category_name";

        public static string INSERT_CATEGORY_SQL = @"INSERT INTO railway.Category
                                                    (`category_id`,
                                                    `category_name`)
                                                    VALUES
                                                    (@category_id,
                                                    @category_name);";
    }
}
