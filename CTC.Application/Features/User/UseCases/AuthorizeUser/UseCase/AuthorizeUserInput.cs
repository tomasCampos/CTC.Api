using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase
{
    public sealed class AuthorizeUserInput : IInput
    {
        public AuthorizeUserInput(in string? userEmail, in string? userPassword)
        {
            UserEmail = userEmail;
            UserPassword = userPassword;
        }

        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
        public UserPermission RequestUserPermission => UserPermission.Unknown;
    }
}
