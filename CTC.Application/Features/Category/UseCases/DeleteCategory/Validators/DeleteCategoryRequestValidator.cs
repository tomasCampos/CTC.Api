using CTC.Application.Features.Category.UseCases.DeleteCategory.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.DeleteCategory.Validators
{
    internal sealed class DeleteCategoryRequestValidator : IRequestValidator<DeleteCategoryInput>
    {
        public Task<RequestValidationModel> Validate(DeleteCategoryInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CategoryId))
                errors.Add("O identificador da categoria informada é inválido");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
