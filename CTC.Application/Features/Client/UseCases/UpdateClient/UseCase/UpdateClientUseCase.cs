using CTC.Application.Features.Client.UseCases.UpdateClient.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.UpdateClient.UseCase
{
    internal sealed class UpdateClientUseCase : IUseCase<UpdateClientInput, Output>
    {
        private readonly IRequestValidator<UpdateClientInput> _validator;
        private readonly IUpdateClientRepository _repository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public UpdateClientUseCase(IRequestValidator<UpdateClientInput> validator, IUpdateClientRepository repository, IUseCaseAuthorizationService useCaseUuthorizationService)
        {
            _validator = validator;
            _repository = repository;
            _useCaseAuthorizationService = useCaseUuthorizationService;
        }

        public async Task<Output> Execute(UpdateClientInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(UpdateClientUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var currentClient = await _repository.GetClientById(input.Id!);
            if (currentClient == null)
                return Output.CreateInvalidParametersResult("O Fornecedor a ser atualizado não existe");

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var clientModel = new ClientModel(input.Id!, currentClient.PersonId!, input.Name!, input.Email!, input.Phone!, input.Document!);
            var result = await _repository.UpdateClient(clientModel);
            if (result < 1)
                return Output.CreateInternalErrorResult("Erro ao atualizar o cliente. Tente novamente mais tarde ou entre em contato com o administrador.");

            return Output.CreateOkResult();
        }

        private async Task<(bool success, string errorMessage)> VerifyIfThereAreUsersWithTheSameUniqueData(UpdateClientInput input)
        {
            var usersWithTheSamePhone = await _repository.GetClientsByPhone(input.Phone!);
            if (usersWithTheSamePhone.Count > 1 || usersWithTheSamePhone.Any(s => s.ClientId != s.ClientId))
                return (false, "Já existe um usuário com o telefone informado");

            var usersWithTheSameEmail = await _repository.GetClientsByEmail(input.Email!);
            if (usersWithTheSameEmail.Count > 1 || usersWithTheSameEmail.Any(s => s.ClientId != s.ClientId))
                return (false, "Já existe um usuário com o email informado");

            var usersWithTheSameDocument = await _repository.GetClientsByDocument(input.Document!);
            if (usersWithTheSameDocument.Count > 1 || usersWithTheSameDocument.Any(s => s.ClientId != s.ClientId))
                return (false, "Já existe um usuário com o documento informado");

            return (true, string.Empty);
        }
    }
}
