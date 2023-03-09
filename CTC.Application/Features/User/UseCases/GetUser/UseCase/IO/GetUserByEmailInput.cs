namespace CTC.Application.Features.User.UseCases.GetUser.UseCase.IO
{
    public sealed class GetUserByEmailInput : IGetUserInput
    {
        public GetUserByEmailInput(in string? parameter)
        {
            Parameter = parameter;
        }

        public GetUserInputParameterType GetUserInputParameterType => GetUserInputParameterType.Email;
        public string? Parameter { get; set; }
    }
}
