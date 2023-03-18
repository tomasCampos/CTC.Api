using CTC.Application.Features.Client.UseCases.DeleteClient.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.DeleteClient.Validators
{
    internal sealed class DeleteClientRequestValidator : IRequestValidator<DeleteClientInput>
    {
        public Task<RequestValidationModel> Validate(DeleteClientInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.ClientId))
                errors.Add("O identificador do cliente informado é inválido");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
