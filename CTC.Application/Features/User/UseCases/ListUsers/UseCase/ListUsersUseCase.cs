using CTC.Application.Features.User.UseCases.ListUsers.Data;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.ListUsers.UseCase
{
    internal sealed class ListUsersUseCase : IUseCase<ListUsersUseCaseInput, Output>
    {
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;
        private readonly IListUsersRepository _usersRepository;

        public ListUsersUseCase(IUseCaseAuthorizationService useCaseAuthorizationService, IListUsersRepository usersRepository)
        {
            _useCaseAuthorizationService = useCaseAuthorizationService;
            _usersRepository = usersRepository;
        }

        public async Task<Output> Execute(ListUsersUseCaseInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(ListUsersUseCase), input.RequestUserPermission);
            if (!isAuthorized)
                return Output.CreateForbiddenResult();

            var result = await _usersRepository.ListUsers(input.Request);
            return Output.CreateOkResult(result);
        }
    }
}