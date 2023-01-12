using System.Net;

namespace CTC.Application.Shared.UseCase.IO
{
    public interface IOutput
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public object? Body { get; set; }
    }
}
