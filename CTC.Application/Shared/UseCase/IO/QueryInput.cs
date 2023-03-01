using CTC.Application.Shared.Request;

namespace CTC.Application.Shared.UseCase.IO
{
    public abstract class QueryInput : IInput
    {
        public QueryInput(QueryRequest request)
        {
            Request = request;
        }

        public QueryRequest Request { get; }
    }
}
