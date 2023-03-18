using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase;
using CTC.Application.Shared.Request.Validator;
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
            if (!string.IsNullOrWhiteSpace(request.Document))
            {
                if(request.Document.Length < 11)
                    errors.Add("O número do documento do fornecedor deve conter pelo menos 11 dígitos");
                if (!request.Document.IsDigitsOnly())
                    errors.Add("O número do documento do fornecedor deve conter apenas caracteres numéricos");
            }              
            
            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
