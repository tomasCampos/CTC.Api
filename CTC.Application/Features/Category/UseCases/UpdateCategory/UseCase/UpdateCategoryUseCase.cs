using CTC.Application.Features.Category.UseCases.UpdateCategory.Data;
using CTC.Application.Features.Client;
using CTC.Application.Features.Client.UseCases.UpdateClient.Data;
using CTC.Application.Features.Client.UseCases.UpdateClient.UseCase;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Category.UseCases.UpdateCategory.UseCase
{
    internal sealed class UpdateCategoryUseCase
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
                //TODO: Validar se o nome do input já existe para outra categoria no banco de dados, caso exista, retornar Bad Request (CreateInvalidParameterResult)
            }

            var categoryModel = new CategoryModel(input.Id!, input.Name!);
            var result = await _repository.UpdateCategory(categoryModel);
            if (result < 1)
                return Output.CreateInternalErrorResult("Erro ao atualizar a categoria. Tente novamente mais tarde ou entre em contato com o administrador.");

            return Output.CreateOkResult();
        }
    }
}
