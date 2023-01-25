using CTC.Application.Shared.Request;

namespace CTC.Api.Features.User.Contracts
{
    public sealed class RegisterUserRequest
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
