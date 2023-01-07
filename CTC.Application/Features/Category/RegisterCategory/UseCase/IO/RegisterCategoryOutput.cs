using CTC.Application.Shared.UseCase.IO;
using System.Net;

namespace CTC.Application.Features.Category.RegisterCategory.UseCase.IO
{
    public sealed class RegisterCategoryOutput : IOutput
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public object? Body { get; set; }
    }
}
