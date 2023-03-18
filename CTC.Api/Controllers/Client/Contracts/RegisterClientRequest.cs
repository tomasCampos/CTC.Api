namespace CTC.Api.Controllers.Client.Contracts
{
    public class RegisterClientRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
    }
}
