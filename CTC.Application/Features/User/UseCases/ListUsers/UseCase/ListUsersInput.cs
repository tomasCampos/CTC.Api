using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.ListUsers.UseCase
{
    public sealed class ListUsersInput : QueryInput
    {
        public ListUsersInput(QueryRequest request) : base(request) {}
    }
}
