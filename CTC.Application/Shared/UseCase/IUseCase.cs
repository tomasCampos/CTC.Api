using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Shared.UseCase
{
    public interface IUseCase<TInput, TOutput> where TInput : IInput where TOutput : IOutput
    {
        Task<TOutput> Execute(TInput input);
    }
}
