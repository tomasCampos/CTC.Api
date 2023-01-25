using CTC.Application.Features.User.Models;

namespace CTC.Application.Shared.UseCase.IO
{
    public interface IInput
    {
        UserPermission RequestUserPermission { get; }
    }
}
