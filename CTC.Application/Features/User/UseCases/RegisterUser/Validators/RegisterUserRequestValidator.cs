using CTC.Application.Features.User.UseCases.RegisterUser.UseCase.IO;
using CTC.Application.Shared.Request;
using System.Collections.Generic;

namespace CTC.Application.Features.User.UseCases.RegisterUser.Validators
{
    internal sealed class RegisterUserRequestValidator : IRequestValidator<RegisterUserInput>
    {
        public RequestValidationModel Validate(RegisterUserInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.UserLastName))
                errors.Add("O último nome do usuário deve ser informado");
            if (!request.UserPermission.HasValue)
                errors.Add("A permissão do usuário deve ser informada");
            if (string.IsNullOrWhiteSpace(request.UserPassword))
                errors.Add("A senha do usuário deve ser informada");
            if (string.IsNullOrWhiteSpace(request.UserFirstName))
                errors.Add("O primeiro nome do usuário deve ser informado");
            if (string.IsNullOrWhiteSpace(request.UserEmail))
                errors.Add("O E-mail do usuário deve ser informado");

            return new RequestValidationModel(errors);
        }
    }
}
