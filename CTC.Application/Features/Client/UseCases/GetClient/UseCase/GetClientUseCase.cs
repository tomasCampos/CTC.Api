using CTC.Application.Features.Client.UseCases.GetClient.Data;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.GetClient.UseCase
{
    internal sealed class GetClientUseCase : IUseCase<GetClientInput, Output>
    {
        private readonly IGetClientRepository _repository;

        public GetClientUseCase(IGetClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Output> Execute(GetClientInput input)
        {
            var result = await _repository.GetClientById(input.ClientId);
            return Output.CreateOkResult(result);
        }
    }
}
