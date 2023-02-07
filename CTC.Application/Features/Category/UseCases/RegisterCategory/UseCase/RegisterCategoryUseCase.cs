using CTC.Application.Features.Category.UseCases.RegisterCategory.Data;
using CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase.IO;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using System.Net;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase
{
    internal sealed class RegisterCategoryUseCase : IUseCase<RegisterCategoryInput, RegisterCategoryOutput>
    {
        private readonly IRequestValidator<RegisterCategoryInput> _validator;
        private readonly IRegisterCategoryRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public RegisterCategoryUseCase(IRequestValidator<RegisterCategoryInput> validator, IRegisterCategoryRepository repository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<RegisterCategoryOutput> Execute(RegisterCategoryInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterCategoryUseCase), input.RequestUserPermission);
            if (!isAuthorized)
            {
                return new RegisterCategoryOutput
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    ValidationErrorMessage = "Falta de permissão para realizar tal ação"
                };
            }

            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                return new RegisterCategoryOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ValidationErrorMessage = validationResult.ErrorMessage
                };
            }

            if (await _repository.CountCategoryByName(input.CategoryName!) > 0)
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
                StatusCode = HttpStatusCode.Created
            };
        }
    }
}
