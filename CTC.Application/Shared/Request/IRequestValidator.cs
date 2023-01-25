using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Shared.Request
{
    internal interface IRequestValidator<TRequest> where TRequest : IInput
    {
        RequestValidationModel Validate(TRequest request);
    }
}
