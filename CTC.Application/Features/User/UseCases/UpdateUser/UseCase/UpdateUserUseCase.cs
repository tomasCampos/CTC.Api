using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.UseCase
{
    internal sealed class UpdateUserUseCase : IUseCase<UpdateUserInput, Output>
    {
        public Task<Output> Execute(UpdateUserInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
