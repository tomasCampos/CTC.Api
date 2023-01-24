using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase.IO
{
    public interface IGetUserInput : IInput
    {
        GetUserInputParameterType GetUserInputParameterType { get; }
        string? Parameter { get; set; }
    }
}