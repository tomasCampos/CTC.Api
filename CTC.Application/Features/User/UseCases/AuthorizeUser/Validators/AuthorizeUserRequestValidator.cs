using CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.AuthorizeUser.Validators
{
    internal sealed class AuthorizeUserRequestValidator : IRequestValidator<AuthorizeUserInput>
    {
        public Task<RequestValidationModel> Validate(AuthorizeUserInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.UserEmail))
                errors.Add("O email deve ser informado");
            if (string.IsNullOrWhiteSpace(request.UserPassword))
                errors.Add("A senha deve ser informada");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
