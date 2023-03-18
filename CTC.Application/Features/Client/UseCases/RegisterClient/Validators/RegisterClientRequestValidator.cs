using CTC.Application.Features.Client.UseCases.RegisterClient.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.RegisterClient.Validators
{
    internal sealed class RegisterClientRequestValidator : IRequestValidator<RegisterClientInput>
    {
        public Task<RequestValidationModel> Validate(RegisterClientInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("O nome do client deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.Email))
                errors.Add("O E-mail do client deve ser informado.");
            if (!Regex.IsMatch(request.Email, RegexValidationsConstants.ValidEmailRegex, RegexOptions.IgnoreCase))
                errors.Add("O E-mail do cliente não é válido");
            if (!string.IsNullOrWhiteSpace(request.Document))
            {
                if (request.Document.Length < 11)
                    errors.Add("O número do documento do cliente deve conter pelo menos 11 dígitos");
                if (!request.Document.IsDigitsOnly())
                    errors.Add("O número do documento do cliente deve conter apenas caracteres numéricos");
            }

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
