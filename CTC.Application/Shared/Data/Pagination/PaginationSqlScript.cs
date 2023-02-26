namespace CTC.Application.Shared.Data.Pagination
{
    internal static class PaginationSqlScript
    {
        private static readonly string Count = @"SELECT COUNT(*) {0} {1}";

        public static string GetCountQuery(in string fromAndJoinsStatements, in string whereStatements)
        {
            return string.Format(Count, fromAndJoinsStatements, whereStatements);
        }

        public static (int StartRow, int NumberOfRows) GetLimitParameters(in int pageNumer, in int pageSize)  
        {
            var startRow = pageSize * (pageNumer - 1);
            return (startRow, pageSize);
        }
    }
}
