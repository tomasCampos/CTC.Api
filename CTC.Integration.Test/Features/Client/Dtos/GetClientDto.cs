namespace CTC.Integration.Test.Features.Client.Dtos
{
    internal sealed class GetClientDto
    {
        public string? ClientId { get; set; }
        public string? PersonId { get; set; }
        public string? FirstName { get; set; }
        public string? Phone { get; set; }
        public string? Document { get; set; }
    }
}
