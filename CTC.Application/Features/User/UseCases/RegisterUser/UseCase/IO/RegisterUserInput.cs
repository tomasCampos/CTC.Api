using CTC.Application.Features.User.Models;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.RegisterUser.UseCase.IO
{
    public sealed class RegisterUserInput : IInput
    {
        public string? UserFirstName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserDocument { get; set; }
        public string? UserLastName { get; set; }
        public UserPermission? UserPermission { get; set; }
        public string? UserPassword { get; set; }
    }
}
