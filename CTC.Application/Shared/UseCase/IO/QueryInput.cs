using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request;

namespace CTC.Application.Shared.UseCase.IO
{
    public abstract class QueryInput : IInput
    {
        public QueryInput(QueryRequest request, in UserPermission requestUserPermission)
        {
            Request = request;
            RequestUserPermission = requestUserPermission;
        }

        public QueryRequest Request { get; }

        public UserPermission RequestUserPermission { get; }
    }
}
