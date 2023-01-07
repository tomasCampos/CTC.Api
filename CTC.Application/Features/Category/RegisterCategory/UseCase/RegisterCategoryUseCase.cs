using CTC.Application.Features.Category.RegisterCategory.Repositories;
using CTC.Application.Features.Category.RegisterCategory.UseCase.IO;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;

namespace CTC.Application.Features.Category.RegisterCategory.UseCase
{
    internal sealed class RegisterCategoryUseCase : IUseCase<RegisterCategoryInput, RegisterCategoryOutput>
    {
        private readonly IRequestValidator<RegisterCategoryInput> _validator;
        private readonly IRegisterCategoryRepository _repository;

        public RegisterCategoryUseCase(IRequestValidator<RegisterCategoryInput> validator, IRegisterCategoryRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public RegisterCategoryOutput Execute(RegisterCategoryInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                return new RegisterCategoryOutput
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    Success = false,
                    ValidationErrorMessage = validationResult.ErrorMessage
                };
            }

            var category = new CategoryModel(input.CategoryName!);
            _repository.InsertCategory(category);
            return new RegisterCategoryOutput
            {
                StatusCode = System.Net.HttpStatusCode.Created,
                Success = true
            };
        }
    }
}
