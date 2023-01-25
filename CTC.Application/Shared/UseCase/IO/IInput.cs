using CTC.Application.Shared.Request;

namespace CTC.Application.Shared.UseCase.IO
{
    public interface IInput
    {
        UserPermission RequestUserPermission { get; }
    }
}
