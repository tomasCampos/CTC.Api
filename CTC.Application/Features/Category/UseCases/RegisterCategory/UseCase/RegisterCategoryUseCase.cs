using CTC.Application.Features.Category.UseCases.RegisterCategory.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase
{
    internal sealed class RegisterCategoryUseCase : IUseCase<RegisterCategoryInput, Output>
    {
        private readonly IRequestValidator<RegisterCategoryInput> _validator;
        private readonly IRegisterCategoryRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public RegisterCategoryUseCase(
            IRequestValidator<RegisterCategoryInput> validator, 
            IRegisterCategoryRepository repository, 
            IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(RegisterCategoryInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterCategoryUseCase));
            if (!isAuthorized)            
                return Output.CreateForbiddenResult();            

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)            
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);            

            if (await _repository.CountCategoryByName(input.CategoryName!) > 0)
                return Output.CreateConflictResult("Já existe uma categoria com esse nome.");

            var category = new CategoryModel(input.CategoryName!);
            await _repository.InsertCategory(category);
            return Output.CreateCreatedResult();
        }
    }
}
