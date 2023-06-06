namespace CTC.Integration.Test.Features.User.Dtos
{
    internal sealed class GetUserDto
    {
        public string? UserId { get; set; }
        public string? PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
        public string? LastName { get; set; }
        public int Permission { get; set; }
        public string? Email { get; set; }
    }
}
