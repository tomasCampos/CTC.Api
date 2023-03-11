using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase;
using CTC.Application.Shared.Request;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory.Validators
{
    internal sealed class RegisterCategoryRequestValidator : IRequestValidator<RegisterCategoryInput>
    {
        public Task<RequestValidationModel> Validate(RegisterCategoryInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CategoryName))
                errors.Add("O nome da categoria deve ser informado");

            var result = new RequestValidationModel(errors);
            return Task.FromResult(result);
        }
    }
}
