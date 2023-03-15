using CTC.Application.Features.Supplier.UseCases.RegisterSupplier.Data;
using CTC.Application.Features.User.UseCases.RegisterUser.UseCase;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Request;
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

        public RegisterSupplierUseCase(IRequestValidator<RegisterSupplierInput> validator, IUseCaseAuthorizationService useCaseAuthorizationService, IRegisterSupplierRepository repsitory)
        {
            _validator = validator;
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _repository = repsitory;
        }

        public async Task<Output> Execute(RegisterSupplierInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(RegisterUserUseCase));
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            return Output.CreateCreatedResult();
        }
    }
}
