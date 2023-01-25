using CTC.Application.Shared.Request;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase.IO
{
    public sealed class GetUserByEmailInput : IGetUserInput
    {
        public GetUserByEmailInput(string? parameter, UserPermission requestUserPermission)
        {
            Parameter = parameter;
            RequestUserPermission = requestUserPermission;
        }

        public GetUserInputParameterType GetUserInputParameterType => GetUserInputParameterType.Email;
        public string? Parameter { get; set; }
        public UserPermission RequestUserPermission { get; }
    }
}
