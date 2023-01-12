using CTC.Application.Features.Category.RegisterCategory.Repositories;
using CTC.Application.Features.Category.RegisterCategory.UseCase.IO;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using System.Net;
using System.Threading.Tasks;

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

        public async Task<RegisterCategoryOutput> Execute(RegisterCategoryInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                return new RegisterCategoryOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ValidationErrorMessage = validationResult.ErrorMessage
                };
            }

            if (await _repository.SearchCategoryByName(input.CategoryName!) > 0)
            {
                return new RegisterCategoryOutput
                {
                    StatusCode = HttpStatusCode.Conflict,
                    ValidationErrorMessage = "Já existe uma categoria com esse nome."
                };
            }

            var category = new CategoryModel(input.CategoryName!);
            await _repository.InsertCategory(category);
            return new RegisterCategoryOutput
            {
                StatusCode = HttpStatusCode.Created,
                Body = category
            };
        }
    }
}
