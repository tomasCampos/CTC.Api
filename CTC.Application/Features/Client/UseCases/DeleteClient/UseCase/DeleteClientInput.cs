using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Client.UseCases.DeleteClient.UseCase
{
    public sealed class DeleteClientInput : IInput
    {
        public string? ClientId { get; set; }
    }
}
