namespace CTC.Integration.Test.Features.User.Dtos
{
    internal sealed class ListUserDto
    {
        public int TotalCount { get; set; }

        public List<GetUserDto>? Results { get; set; }
    }
}
