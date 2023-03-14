using CTC.Application.Shared.Authorization;

namespace CTC.Api.Controllers.User.Contracts
{
    public sealed class RegisterUserRequest
    {
        public string? FirstName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
        public string? LastName { get; set; }
        public UserPermission? Permission { get; set; }
        public string? Password { get; set; }
    }
}
