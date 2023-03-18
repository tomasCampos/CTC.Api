using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Client.UseCases.ListClients.UseCase
{
    public sealed class ListClientsInput : QueryInput
    {
        public ListClientsInput(QueryRequest request) : base(request) { }
    }
}
