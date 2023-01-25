using CTC.Application.Features.User.UseCases.GetUser.Data;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.UseCase;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase
{
    internal sealed class GetUserUseCase : IUseCase<IGetUserInput, GetUserOutput>
    {
        private readonly IGetUserRepository _userRepository;

        public GetUserUseCase(IGetUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserOutput> Execute(IGetUserInput input)
        {
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