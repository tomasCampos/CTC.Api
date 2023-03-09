using CTC.Application.Features.User.UseCases.GetUser.Data;
using CTC.Application.Features.User.UseCases.GetUser.UseCase.IO;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using CTC.Application.Shared.UserContext;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.GetUser.UseCase
{
    internal sealed class GetUserUseCase : IUseCase<IGetUserInput, Output>
    {
        private readonly IGetUserRepository _userRepository;
        private readonly IUserContext _userContext;

        public GetUserUseCase(IGetUserRepository userRepository, IUserContext userContext)
        {
            _userRepository = userRepository;
            _userContext = userContext;
        }

        public async Task<Output> Execute(IGetUserInput input)
        {
            if (input.GetUserInputParameterType == GetUserInputParameterType.Email)
            {
                if (!string.IsNullOrEmpty(_userContext.UserEmail) && _userContext.UserEmail != input.Parameter)
                    return Output.CreateForbiddenResult();

                var user = await _userRepository.GetUserByEmail(input.Parameter!);

                if(user == null)
                    return Output.CreateNotFoundResult();

                return Output.CreateOkResult(user);
            }

            if (input.GetUserInputParameterType == GetUserInputParameterType.Token)
            {
                var user = await _userRepository.GetUserByEmail(_userContext.UserEmail);

                if (user == null)
                    return Output.CreateNotFoundResult();

                return Output.CreateOkResult(user);
            }

            return Output.CreateNotFoundResult();
        }
    }
}