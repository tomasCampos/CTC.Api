namespace CTC.Application.Shared.Request
{
    public class QueryRequest
    {
        public int PageNumber { get; }
        public int PageSize { get; }
        public string SearchParam { get; }

        public QueryRequest()
        {
            PageNumber = 1;
            PageSize = 10;
            SearchParam = string.Empty;
        }

        protected QueryRequest(int pageNumber, int pageSize, string searchParam)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
            SearchParam = searchParam;
        }
    }
}