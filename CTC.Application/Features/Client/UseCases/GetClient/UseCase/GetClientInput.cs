using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Client.UseCases.GetClient.UseCase
{
    public class GetClientInput : IInput
    {
        public string ClientId { get; set; }
    }
}
