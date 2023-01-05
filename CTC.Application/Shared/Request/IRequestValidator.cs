namespace CTC.Application.Shared.Request
{
    internal interface IRequestValidator<TRequest>
    {
        RequestValidationModel Validate(TRequest request);
    }
}
