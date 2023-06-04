using System.Net;

namespace CTC.Integration.Test.Shared
{
    internal abstract class DtoBase
    {
        HttpStatusCode StatusCode { get; }
        public string? ValidationErrorMessage { get; }
    }
}
