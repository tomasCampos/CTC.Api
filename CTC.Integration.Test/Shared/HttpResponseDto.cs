using System.Net;

namespace CTC.Integration.Test.Shared
{
    public sealed class HttpResponseDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public T? Body { get; set; }
    }
}