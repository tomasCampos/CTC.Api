using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase
{
    internal sealed class RegisterCostCenterUseCase : IUseCase<RegisterCostCenterInput, Output>
    {
        private readonly IRegisterCostCenterRepository _repository;
        private readonly IRequestValidator<RegisterCostCenterInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public RegisterCostCenterUseCase(
            IRegisterCostCenterRepository repository,
            IRequestValidator<RegisterCostCenterInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _repository = repository;
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(RegisterCostCenterInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterCostCenterUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var nameAlreadyUsed = await _repository.VerifyIfCostCenterAlreadyExists(input.Name!);
            if (nameAlreadyUsed)
                return Output.CreateConflictResult("Já existe um centro de custo com este nome. Informe um nome diferente.");

            var isValidClient = await _repository.VerifyIfCostCenterClientExists(input.ClientId!);
            if (!isValidClient)
                return Output.CreateInvalidParametersResult("O cliente informado não existe.");

            var costCenterModel = new CostCenterModel(input.Name!, input.Observations, input.StartingDate!.Value, input.ExpectedClosingDate, input.ClosingDate, input.ClientId!,
                input.AddressPostalCode, input.AddressStreetName, input.AddressNeighborhood, input.AddressNumber, input.AddressComplement, input.AddressCity, input.AddressState);

            var success = await _repository.InsertCostCenter(costCenterModel);
            if (!success)
                return Output.CreateInternalErrorResult("Não foi possível registrar o centro de custo. Ocorreu um erro. Tente novamente mais tarde.");

            return Output.CreateCreatedResult();
        }
    }
}
