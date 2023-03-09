using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.UpdateUser.UseCase
{
    public sealed class UpdateUserInput : IInput
    {
        public UpdateUserInput(in string? userId, in string? userFirstName, in string? userEmail, in string? userPhone, in string? userDocument, in string? userLastName, in UserPermission? userPermission, in string? userPassword)
        {
            UserId = userId;
            UserFirstName = userFirstName;
            UserEmail = userEmail;
            UserPhone = userPhone;
            UserDocument = userDocument;
            UserLastName = userLastName;
            UserPermission = userPermission;
            UserPassword = userPassword;
        }

        public string? UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserDocument { get; set; }
        public string? UserLastName { get; set; }
        public UserPermission? UserPermission { get; set; }
        public string? UserPassword { get; set; }
    }
}
