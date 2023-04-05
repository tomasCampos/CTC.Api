namespace CTC.Application.Features.Category.UseCases
{
    internal static class CategorySqlScripts
    {
        public static string COUNT_CATEGORY_BY_NAME = "SELECT COUNT(*) FROM heroku_3a06699194dd49a.Category WHERE category_name = @category_name";

        public static string INSERT_CATEGORY = @"INSERT INTO heroku_3a06699194dd49a.Category
                                                    (`category_id`,
                                                    `category_name`)
                                                    VALUES
                                                    (@category_id,
                                                    @category_name);";

        public static string GET_CATEGORY_BY_ID = @"SELECT
                                                        `category_id` AS Id,
                                                        `category_name` AS Name
                                                    FROM 
                                                        heroku_3a06699194dd49a.Category 
                                                    WHERE 
                                                        category_id = @category_id";

        public static string LIST_CATEGORIES_SELECT_STATEMENT = @"SELECT
                                                                category_id AS CategoryId,
                                                                category_name AS CategoryName";

        public static string LIST_CATEGORIES_FROM_AND_JOIN_STATEMENTS = @"FROM
                                                                    `heroku_3a06699194dd49a`.category";

        public static string LIST_CATEGORY_WHERE_STATEMENT = @"WHERE
                                                            (
                                                            category_name LIKE '%@search_param%'
                                                            )";

        public static string UPDATE_CATEGORY = @"UPDATE
                                                `heroku_3a06699194dd49a`.`category`
                                                SET
                                                `category_name` = @category_name
                                                WHERE
                                                `category_id` = @category_id;";
    }
}
