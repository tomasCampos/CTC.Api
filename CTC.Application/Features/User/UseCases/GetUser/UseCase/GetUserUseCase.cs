using CTC.Application.Features.User.UseCases.GetUser.Data;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase;
using System.Net;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase
{
    internal sealed class GetUserUseCase : IUseCase<IGetUserInput, GetUserOutput>
    {
        private readonly IGetUserRepository _userRepository;
        private readonly IUseCaseAuthorizationService _useCaseAuthorizationService;

        public GetUserUseCase(IGetUserRepository userRepository, IUseCaseAuthorizationService useCaseAuthorizationService)
        {
            _userRepository = userRepository;
            _useCaseAuthorizationService = useCaseAuthorizationService;
        }

        public async Task<GetUserOutput> Execute(IGetUserInput input)
        {
            var isAuthorized = await _useCaseAuthorizationService.Authorize(nameof(GetUserUseCase), input.RequestUserPermission);
            if (!isAuthorized)
            {
                return new GetUserOutput
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

            return new GetUserOutput
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Body = user
            };
        }
    }
}