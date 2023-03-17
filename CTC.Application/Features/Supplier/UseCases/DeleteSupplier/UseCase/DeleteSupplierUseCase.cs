using CTC.Application.Features.Supplier.UseCases.DeleteSupplier.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Supplier.UseCases.DeleteSupplier.UseCase
{
    internal sealed class DeleteSupplierUseCase : IUseCase<DeleteSupplierInput, Output>
    {
        private readonly IRequestValidator<DeleteSupplierInput> _validator;
        private readonly IDeleteSupplierRepository _deleteSupplierRepository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private const string ErrorMessage = "Falha ao excluir fornecedor. Contate o administrador";

        public DeleteSupplierUseCase(IRequestValidator<DeleteSupplierInput> validator, IDeleteSupplierRepository deleteSupplierRepository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _deleteSupplierRepository = deleteSupplierRepository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(DeleteSupplierInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteSupplierUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var user = await _deleteSupplierRepository.GetSupplierById(input.SupplierId!);
            if (user == null)
                return Output.CreateInvalidParametersResult("O fornecedor a ser excluído não existe.");

            var deleteUserInSqlResult = await _deleteSupplierRepository.DeleteSupplier(user.SupplierId!, user.PersonId!);
            if (!deleteUserInSqlResult)
                return Output.CreateInternalErrorResult(ErrorMessage);

            return Output.CreateOkResult();
        }
    }
}
