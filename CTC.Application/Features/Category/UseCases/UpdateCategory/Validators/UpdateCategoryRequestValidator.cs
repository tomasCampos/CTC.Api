using CTC.Application.Features.Category.UseCases.UpdateCategory.UseCase;
using CTC.Application.Shared.Request.Validator;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory.Validators
{
    internal sealed class UpdateCategoryRequestValidator : IRequestValidator<UpdateCategoryInput>
    {
        public Task<RequestValidationModel> Validate(UpdateCategoryInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.Id))
                errors.Add("O Id da categoria deve ser informado");
            if (string.IsNullOrWhiteSpace(request.Name))
                errors.Add("O nome da categoria deve ser informado");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
