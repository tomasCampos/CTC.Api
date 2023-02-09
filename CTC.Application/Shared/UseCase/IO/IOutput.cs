using System.Net;

namespace CTC.Application.Shared.UseCase.IO
{
    public class Output
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public object? Body { get; set; }
    }
}
