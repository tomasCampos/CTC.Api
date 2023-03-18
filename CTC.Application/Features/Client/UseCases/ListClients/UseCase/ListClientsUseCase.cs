using CTC.Application.Features.Client.UseCases.ListClients.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.ListClients.UseCase
{
    internal sealed class ListClientsUseCase : IUseCase<ListClientsInput, Output>
    {
        private readonly IListClientsRepository _clientsRepository;

        public ListClientsUseCase(IUseCaseAuthorizationService useCaseAuthorizationService, IListClientsRepository clientsRepository)
        {
            _clientsRepository = clientsRepository;
        }

        public async Task<Output> Execute(ListClientsInput input)
        {
            var result = await _clientsRepository.ListClients(input.Request);
            return Output.CreateOkResult(result);
        }
    }
}
