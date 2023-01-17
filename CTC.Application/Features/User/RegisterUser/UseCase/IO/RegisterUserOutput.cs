using CTC.Application.Shared.UseCase.IO;
using System.Net;

namespace CTC.Application.Features.User.RegisterUser.UseCase.IO
{
    public class RegisterUserOutput : IOutput
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public object? Body { get; set; }
    }
}
