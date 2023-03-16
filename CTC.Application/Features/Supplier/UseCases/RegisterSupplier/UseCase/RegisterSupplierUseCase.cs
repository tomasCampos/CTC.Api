using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.RegisterSupplier.UseCase
{
    internal sealed class RegisterSupplierUseCase : IUseCase<RegisterSupplierInput, Output>
    {
        private readonly IRequestValidator<RegisterSupplierInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IRegisterSupplierRepository _repository;

        public RegisterSupplierUseCase(
            IRequestValidator<RegisterSupplierInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IRegisterSupplierRepository repsitory)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _repository = repsitory;
        }

        public async Task<Output> Execute(RegisterSupplierInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterSupplierUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var userAlreadyExists = await _repository.VerifyIfSupplierAlreadyExists(input.Email!, input.Phone!, input.Document!) > 0;
            if (userAlreadyExists)
                return Output.CreateConflictResult("Já existe um fornecedor cadastrado com o email, telefone ou documento informados");

            var supplier = new SupplierModel(input.Name!, input.Email!, input.Phone!, input.Document!);
            var wasSupplierInsertedWithSuccess = await _repository.InsertSupplier(supplier);
            if (!wasSupplierInsertedWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível cadastrar o fornecedor. Tente novamente mais tarde.");

            return Output.CreateCreatedResult();
        }
    }
}
