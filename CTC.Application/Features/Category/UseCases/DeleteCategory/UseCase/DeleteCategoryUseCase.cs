using CTC.Application.Features.Category.UseCases.DeleteCategory.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.DeleteCategory.UseCase
{
    internal class DeleteCategoryUseCase : IUseCase<DeleteCategoryInput, Output>
    {
        private readonly IRequestValidator<DeleteCategoryInput> _validator;
        private readonly IDeleteCategoryRepository _deleteCategoryRepository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private const string ErrorMessage = "Falha ao excluir categoria. Contate o administrador";

        public DeleteCategoryUseCase(IRequestValidator<DeleteCategoryInput> validator, IDeleteCategoryRepository deleteCategoryRepository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _deleteCategoryRepository = deleteCategoryRepository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(DeleteCategoryInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteCategoryUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var category = await _deleteCategoryRepository.GetCategoryById(input.CategoryId!);
            if (category == null)
                return Output.CreateInvalidParametersResult("A categoria a ser excluída não existe.");

            var deleteUserInSqlResult = await _deleteCategoryRepository.DeleteCategory(category.Id!, category.Name!);
            if (!deleteUserInSqlResult)
                return Output.CreateInternalErrorResult(ErrorMessage);

            return Output.CreateOkResult();
        }
    }
}
