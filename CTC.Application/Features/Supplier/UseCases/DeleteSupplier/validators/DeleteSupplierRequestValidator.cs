using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Validators
{
    internal sealed class DeleteSupplierRequestValidator : IRequestValidator<DeleteSupplierInput>
    {
        public Task<RequestValidationModel> Validate(DeleteSupplierInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.SupplierId))
                errors.Add("O identificador do fornecedor informado é inválido");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
