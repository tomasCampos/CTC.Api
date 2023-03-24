using CTC.Application.Features.Supplier.UseCases.UpdateSupplier.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.UpdateSupplier.UseCase
{
    internal sealed class UpdateSupplierUseCase : IUseCase<UpdateSupplierInput, Output>
    {
        private readonly IRequestValidator<UpdateSupplierInput> _validator;
        private readonly IUpdateSupplierRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public UpdateSupplierUseCase(IRequestValidator<UpdateSupplierInput> validator, IUpdateSupplierRepository repository, IUseCaseAuthorizationService useCaseUuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseUuthorizationService;
        }

        public async Task<Output> Execute(UpdateSupplierInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateSupplierUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var currentSupplier = await _repository.GetSupplierById(input.Id!);
            if (currentSupplier == null)
                return Output.CreateInvalidParametersResult("O Fornecedor a ser atualizado não existe");

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var uniqueDataVerificationResult = await VerifyIfThereAreUsersWithTheSameUniqueData(input);
            if (!uniqueDataVerificationResult.success)
                return Output.CreateInvalidParametersResult(uniqueDataVerificationResult.errorMessage);

            var supplierModel = new SupplierModel(input.Id!, currentSupplier.PersonId!, input.Name!, input.Email!, input.Phone!, input.Document!);
            var result = await _repository.UpdateSupplier(supplierModel);
            if (result < 1)
                return Output.CreateInternalErrorResult("Erro ao atualizar o fornecedor. Tente novamente mais tarde ou entre em contato com o administrador.");

            return Output.CreateOkResult();
        }

        private async Task<(bool success, string errorMessage)> VerifyIfThereAreUsersWithTheSameUniqueData(UpdateSupplierInput input)
        {
            var usersWithTheSamePhone = await _repository.GetSuppliersByPhone(input.Phone!);
            if (usersWithTheSamePhone.Count > 1 || usersWithTheSamePhone.Any(s => s.SupplierId != s.SupplierId))
                return (false, "Já existe um usuário com o telefone informado");

            var usersWithTheSameEmail = await _repository.GetSuppliersByEmail(input.Email!);
            if (usersWithTheSameEmail.Count > 1 || usersWithTheSameEmail.Any(s => s.SupplierId != s.SupplierId))
                return (false, "Já existe um usuário com o email informado");

            var usersWithTheSameDocument = await _repository.GetSuppliersByDocument(input.Document!);
            if (usersWithTheSameDocument.Count > 1 || usersWithTheSameDocument.Any(s => s.SupplierId != s.SupplierId))
                return (false, "Já existe um usuário com o documento informado");

            return (true, string.Empty);
        }
    }
}
