using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Shared.UseCase
{
    internal interface IUseCase<TInput, TOutput> where TInput : IInput where TOutput : IOutput
    {
        TOutput Execute(TInput input);
    }
}
