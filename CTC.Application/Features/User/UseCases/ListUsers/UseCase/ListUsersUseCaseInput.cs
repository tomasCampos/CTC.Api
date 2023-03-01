using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.ListUsers.UseCase
{
    public sealed class ListUsersUseCaseInput : QueryInput
    {
        public ListUsersUseCaseInput(QueryRequest request) : base(request) {}
    }
}
