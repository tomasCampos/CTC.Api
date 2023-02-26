using System.Collections.Generic;

namespace CTC.Application.Shared.Data.Pagination
{
    internal sealed class PaginatedQueryResult<T> where T : class
    {
        public int TotalCount { get; }
        public IEnumerable<T> Results { get; }

        public PaginatedQueryResult()
        {
            Results = new List<T>(0);
            TotalCount = 0;
        }

        public PaginatedQueryResult(IEnumerable<T> results, int totalCount)
        {
            Results = results;
            TotalCount = totalCount;
        }
    }
}
