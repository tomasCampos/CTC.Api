namespace CTC.Integration.Test.Features.Client.Dtos
{
    internal sealed class ListClientDto
    {
        public int TotalCount { get; set; }

        public List<GetClientDto>? Results { get; set; }
    }
}
