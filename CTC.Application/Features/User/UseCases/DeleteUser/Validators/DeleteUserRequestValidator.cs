using CTC.Application.Features.User.UseCases.DeleteUser.UseCase;
using CTC.Application.Shared.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.DeleteUser.Validators
{
    internal sealed class DeleteUserRequestValidator : IRequestValidator<DeleteUserInput>
    {
        public Task<RequestValidationModel> Validate(DeleteUserInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.UserId))
                errors.Add("O identificador do usuário informado é inválido");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
