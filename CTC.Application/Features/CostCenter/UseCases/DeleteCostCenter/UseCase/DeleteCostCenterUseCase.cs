using CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.DeleteCostCenter.UseCase
{
    internal sealed class DeleteCostCenterUseCase : IUseCase<DeleteCostCenterInput, Output>
    {
        private readonly IDeleteCostCenterRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public DeleteCostCenterUseCase(IDeleteCostCenterRepository repository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _repository = repository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(DeleteCostCenterInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteCostCenterUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var costCenterToDelete = await _repository.GetCostCenterById(input.CostCenterId);
            if (costCenterToDelete == null)
                return Output.CreateInvalidParametersResult("O centro de custo a ser excluído não existe.");

            var result = await _repository.DeleteCostCenter(input.CostCenterId, costCenterToDelete.AddressId);

            if (!result)
                return Output.CreateInternalErrorResult("Erro ao excluir centro de custo. Tente novamente mais tarde.");

            return Output.CreateOkResult();
        }
    }
}
