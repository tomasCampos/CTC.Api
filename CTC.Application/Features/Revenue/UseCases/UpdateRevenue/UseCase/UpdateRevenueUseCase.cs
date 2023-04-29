using CTC.Application.Features.Revenue.UseCases.UpdateRevenue.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.UpdateRevenue.UseCase
{
    internal sealed class UpdateRevenueUseCase : IUseCase<UpdateRevenueInput, Output>
    {
        private readonly IRequestValidator<UpdateRevenueInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IUpdateRevenueRepository _repository;

        public UpdateRevenueUseCase(
            IRequestValidator<UpdateRevenueInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IUpdateRevenueRepository repository)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _repository = repository;
        }

        public async Task<Output> Execute(UpdateRevenueInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateRevenueUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var transactionId = await _repository.GetTransactionIdByRevenueId(input.RevenueId!);
            if (string.IsNullOrEmpty(transactionId))
                return Output.CreateInvalidParametersResult("A receita a ser alterada não existe.");

            var clientId = await _repository.GetClientIdByCostCenterId(input.CostCenterId!);
            if (clientId == null)
                return Output.CreateInvalidParametersResult("O centro de custo informado não é válido ou não possui um cliente relacionado");

            var revenue = new RevenueModel(clientId, input.Value!.Value, input.PaymentDate, input.Observation,
                input.CategoryId, input.CostCenterId!, transactionId: transactionId, revenueId: input.RevenueId);
            var wasRevenueUpdatedWithSuccess = await _repository.UpdateRevenue(revenue);
            if (!wasRevenueUpdatedWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível atualizar a receita. Tente novamente mais tarde.");

            return Output.CreateOkResult();
        }
    }
}
