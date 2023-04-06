using CTC.Application.Features.Category.UseCases.UpdateCategory.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory.UseCase
{
    internal sealed class UpdateCategoryUseCase : IUseCase<UpdateCategoryInput, Output>
    {
        private readonly IRequestValidator<UpdateCategoryInput> _validator;
        private readonly IUpdateCategoryRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public UpdateCategoryUseCase(IRequestValidator<UpdateCategoryInput> validator, IUpdateCategoryRepository repository, IUseCaseAuthorizationService useCaseUuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseUuthorizationService;
        }

        public async Task<Output> Execute(UpdateCategoryInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateCategoryUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var currentCategory = await _repository.GetCategoryById(input.Id!);
            if (currentCategory == null)
                return Output.CreateInvalidParametersResult("A categoria a ser atualizada não existe");

            if (!string.Equals(input.Name, currentCategory.Name))
            {
                var categoryCount = await _repository.CountCategoryByName(input.Name!);

                if (categoryCount > 0)
                    return Output.CreateConflictResult("O nome informado já está sendo usado para outra categoria");
            }

            var categoryModel = new CategoryModel(input.Name!, input.Id!);
            var result = await _repository.UpdateCategory(categoryModel);
            if (result < 1)
                return Output.CreateInternalErrorResult("Erro ao atualizar a categoria. Tente novamente mais tarde ou entre em contato com o administrador.");

            return Output.CreateOkResult();
        }
    }
}
