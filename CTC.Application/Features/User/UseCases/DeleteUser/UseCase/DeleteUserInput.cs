using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.User.UseCases.DeleteUser.UseCase
{
    public sealed class DeleteUserInput : IInput
    {
        public string? UserId { get; set; }
    }
}
