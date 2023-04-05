using CTC.Application.Features.CostCenter.UseCases.RegisterCostCenter.UseCase;
using CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.CostCenter.UseCases.UpdateCostCenter.UseCase
{
    internal sealed class UpdateCostCenterUseCase : IUseCase<UpdateCostCenterInput, Output>
    {
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IUpdateCostCenterRepository _repository;
        private readonly IRequestValidator<UpdateCostCenterInput> _validator;

        public UpdateCostCenterUseCase(IUseCaseAuthorizationService authorizationService, IUpdateCostCenterRepository repository, IRequestValidator<UpdateCostCenterInput> requestValidator)
        {
            _useCaseAuthorizationService = authorizationService;
            _repository = repository;
            _validator = requestValidator;
        }

        public async Task<Output> Execute(UpdateCostCenterInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateCostCenterUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var currentCostCenter = await _repository.GetCostCenterById(input.Id!);
            if (currentCostCenter == null)
                return Output.CreateInvalidParametersResult("O centro de custo a ser atualizado não existe.");

            if(!string.Equals(currentCostCenter.Name, input.Name!))
            {
                var nameAlreadyUsed = await _repository.VerifyIfCostCenterNameIsAlreadyUsed(input.Name!);
                if (nameAlreadyUsed)
                    return Output.CreateConflictResult("Já existe um centro de custo com este nome. Informe um nome diferente.");
            }

            if (!string.Equals(currentCostCenter.ClientId, input.ClientId))
            {
                var isValidClient = await _repository.VerifyIfClientExists(input.ClientId!);
                if (!isValidClient)
                    return Output.CreateInvalidParametersResult("O cliente informado não existe.");
            }

            var newCostCenter = new CostCenterModel(input.Name!, input.Observations, input.StartingDate!.Value, input.ExpectedClosingDate, input.ClosingDate,
                input.ClientId!, input.AddressPostalCode, input.AddressStreetName, input.AddressNeighborhood, input.AddressNumber, input.AddressComplement,
                input.AddressCity, input.AddressState, input.Id, currentCostCenter.AddressId);

            var updateResult = await _repository.UpdateCostCenter(newCostCenter);

            if (!updateResult)
                return Output.CreateInternalErrorResult("Não foi possível atualizar o centro de custo. Ocorreu um erro. Tente novamente mais tarde.");

            return Output.CreateOkResult();
        }
    }
}
