using CTC.Application.Features.User.UseCases.UpdateUser.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.UpdateUser.Validators
{
    internal class UpdateUserRequestValidator : IRequestValidator<UpdateUserInput>
    {
        public Task<RequestValidationModel> Validate(UpdateUserInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.UserId))
                errors.Add("O Id do usuário deve ser informado");
            if (string.IsNullOrWhiteSpace(request.UserLastName))
                errors.Add("O último nome do usuário deve ser informado");
            if (!request.UserPermission.HasValue)
                errors.Add("A permissão do usuário deve ser informada");
            if (string.IsNullOrWhiteSpace(request.UserPassword))
                errors.Add("A senha do usuário deve ser informada");
            if (request.UserPassword!.Length < 6)
                errors.Add("A senha do usuário deve conter pelo menos 6 dígitos");
            if (string.IsNullOrWhiteSpace(request.UserFirstName))
                errors.Add("O primeiro nome do usuário deve ser informado");
            if (string.IsNullOrWhiteSpace(request.UserEmail))
                errors.Add("O E-mail do usuário deve ser informado");
            if (!Regex.IsMatch(request.UserEmail, RegexValidationsConstants.ValidEmailRegex, RegexOptions.IgnoreCase))
                errors.Add("O E-mail do usuário não é válido");
            if (string.IsNullOrWhiteSpace(request.UserDocument) || request.UserDocument.Length < 11)
                errors.Add("O número do documento do usuário deve conter pelo menos 11 dígitos");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
