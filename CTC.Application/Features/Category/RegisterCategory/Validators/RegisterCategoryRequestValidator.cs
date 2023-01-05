using CTC.Application.Features.Category.RegisterCategory.UseCase.IO;
using CTC.Application.Shared.Request;
using System.Collections.Generic;

namespace CTC.Application.Features.Category.RegisterCategory.Validators
{
    internal sealed class RegisterCategoryRequestValidator : IRequestValidator<RegisterCategoryInput>
    {
        public RequestValidationModel Validate(RegisterCategoryInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.CategoryName))
                errors.Add("O nome da categoria deve ser informado");

            return new RequestValidationModel(errors);
        }
    }
}
