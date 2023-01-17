using CTC.Application.Features.User.RegisterUser.UseCase.IO;
using CTC.Application.Shared.UseCase;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.RegisterUser.UseCase
{
    internal sealed class RegisterUserUseCase : IUseCase<RegisterUserInput, RegisterUserOutput>
    {
        public Task<RegisterUserOutput> Execute(RegisterUserInput input)
        {
            //validar
            //cadastrar no firebase
            //cadastrar no banco
            throw new System.NotImplementedException();
        }
    }
}
