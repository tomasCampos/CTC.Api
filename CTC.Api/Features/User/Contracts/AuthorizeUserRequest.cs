namespace CTC.Api.Features.User.Contracts
{
    public sealed class AuthorizeUserRequest
    {
        public string? UserEmail { get; set; }
        public string? UserPassword { get; set; }
    }
}
