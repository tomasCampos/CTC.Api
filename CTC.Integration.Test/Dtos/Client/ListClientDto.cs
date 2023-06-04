namespace CTC.Integration.Test.Dtos.Client
{
    internal sealed class ListClientDto
    {
        public int TotalCount { get; set; }

        public List<GetClientDto>? Results { get; set; }
    }
}
