using CTC.Application.Shared.UseCase.IO;
using System.Net;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase.IO
{
    internal sealed class GetUserOutput : IOutput
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public object? Body { get; set; }
    }
}
