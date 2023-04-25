using CTC.Application.Features.Revenue.UseCases.RegisterRevenue.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Revenue.UseCases.RegisterRevenue.UseCase
{
    internal sealed class RegisterRevenueUseCase : IUseCase<RegisterRevenueInput, Output>
    {
        private readonly IRequestValidator<RegisterRevenueInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IRegisterRevenueRepository _repository;

        public RegisterRevenueUseCase(
            IRequestValidator<RegisterRevenueInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IRegisterRevenueRepository repository)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            this._repository = repository;
        }

        public async Task<Output> Execute(RegisterRevenueInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterRevenueUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var revenue = new RevenueModel(input.ClientId!, input.Value!.Value, input.PaymentDate, input.Observation, input.CategoryId, input.CostCenterId!);
            var wasRevenueInsertedWithSuccess = await _repository.InsertRevenue(revenue);
            if (!wasRevenueInsertedWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível cadastrar a receita. Tente novamente mais tarde.");

            return Output.CreateCreatedResult();
        }
    }
}
