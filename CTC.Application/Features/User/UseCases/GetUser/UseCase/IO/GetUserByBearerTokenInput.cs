namespace CTC.Application.Features.User.UseCases.GetUser.UseCase.IO
{
    public sealed class GetUserByBearerTokenInput : IGetUserInput
    {
        public GetUserByBearerTokenInput()
        {
            Parameter = null;
        }

        public GetUserInputParameterType GetUserInputParameterType => GetUserInputParameterType.Token;
        public string? Parameter { get; set; }
    }
}
