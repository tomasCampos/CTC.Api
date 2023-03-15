using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase.Validation;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Validators
{
    internal sealed class RegisterSupplierRequestValidator : IRequestValidator<RegisterSupplierInput>
    {
        public Task<RequestValidationModel> Validate(RegisterSupplierInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("O nome do fornecedor deve ser informado.");
            if (string.IsNullOrWhiteSpace(request.Email))
                errors.Add("O E-mail do fornecedor deve ser informado.");
            if (!Regex.IsMatch(request.Email, RegexValidationsConstants.ValidEmailRegex, RegexOptions.IgnoreCase))
                errors.Add("O E-mail do fornecedor não é válido");
            if (!string.IsNullOrWhiteSpace(request.Document) && request.Document.Length < 11)
                errors.Add("O número do documento do usuário deve conter pelo menos 11 dígitos");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
