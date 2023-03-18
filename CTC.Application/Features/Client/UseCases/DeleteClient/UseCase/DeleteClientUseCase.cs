using CTC.Application.Features.Client.UseCases.DeleteClient.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.DeleteClient.UseCase
{
    internal sealed class DeleteClientUseCase : IUseCase<DeleteClientInput, Output>
    {
        private readonly IRequestValidator<DeleteClientInput> _validator;
        private readonly IDeleteClientRepository _deleteClientRepository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private const string ErrorMessage = "Falha ao excluir fornecedor. Contate o administrador";

        public DeleteClientUseCase(IRequestValidator<DeleteClientInput> validator, IDeleteClientRepository deleteClientRepository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _validator = validator;
            _deleteClientRepository = deleteClientRepository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(DeleteClientInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(DeleteClientUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var user = await _deleteClientRepository.GetClientById(input.ClientId!);
            if (user == null)
                return Output.CreateInvalidParametersResult("O fornecedor a ser excluído não existe.");

            var deleteUserInSqlResult = await _deleteClientRepository.DeleteClient(user.ClientId!, user.PersonId!);
            if (!deleteUserInSqlResult)
                return Output.CreateInternalErrorResult(ErrorMessage);

            return Output.CreateOkResult();
        }
    }
}
