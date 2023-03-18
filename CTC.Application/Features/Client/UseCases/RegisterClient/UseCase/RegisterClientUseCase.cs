using CTC.Application.Features.Client.UseCases.RegisterClient.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.Client.UseCases.RegisterClient.UseCase
{
    internal sealed class RegisterClientUseCase : IUseCase<RegisterClientInput, Output>
    {
        private readonly IRequestValidator<RegisterClientInput> _validator;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IRegisterClientRepository _repository;

        public RegisterClientUseCase(
            IRequestValidator<RegisterClientInput> validator,
            IUseCaseAuthorizationService useCaseAuthorizationService,
            IRegisterClientRepository repsitory)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _repository = repsitory;
        }

        public async Task<Output> Execute(RegisterClientInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterClientUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var userAlreadyExists = await _repository.VerifyIfClientAlreadyExists(input.Email!, input.Phone!, input.Document!) > 0;
            if (userAlreadyExists)
                return Output.CreateConflictResult("Já existe um cliente cadastrado com o email, telefone ou documento informados");

            var client = new ClientModel(input.Name!, input.Email!, input.Phone!, input.Document!);
            var wasClientInsertedWithSuccess = await _repository.InsertClient(client);
            if (!wasClientInsertedWithSuccess)
                return Output.CreateInternalErrorResult("Ocorreu um erro e não foi possível cadastrar o cliente. Tente novamente mais tarde.");

            return Output.CreateCreatedResult();
        }
    }
}
