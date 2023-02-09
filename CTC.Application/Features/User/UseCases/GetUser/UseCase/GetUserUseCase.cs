using CTC.Application.Features.User.UseCases.GetUser.Data;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Net;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase
{
    internal sealed class GetUserUseCase : IUseCase<IGetUserInput, Output>
    {
        private readonly IGetUserRepository _userRepository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public GetUserUseCase(IGetUserRepository userRepository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _userRepository = userRepository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<Output> Execute(IGetUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(GetUserUseCase), input.RequestUserPermission);
            if (!isAuthorized)
            {
                return new Output
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    ValidationErrorMessage = "Falta de permissão para realizar tal ação"
                };
            }

            UserModel? user = null;

            if (input.GetUserInputParameterType == GetUserInputParameterType.Email)
            {
                user = await _userRepository.GetUserByEmail(input.Parameter!);
            }

            return new Output
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Body = user
            };
        }
    }
}