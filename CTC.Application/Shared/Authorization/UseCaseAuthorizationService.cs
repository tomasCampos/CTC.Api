using CTC.Application.Shared.UserContext;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Authorization
{
    internal sealed class UseCaseAuthorizationService : IUseCaseAuthorizationService
    {
        private readonly IUserContext _userContext;

        public UseCaseAuthorizationService(IUserContext userContext)
        {
            _userContext = userContext;
        }

        public Task<bool> Authorize(string useCaseName)
        {
            if(_userContext.UserPermission == UserPermission.Administrator)
                return Task.FromResult(true);

            if(string.Equals(useCaseName, "RegisterCategoryUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);           

            if (string.Equals(useCaseName, "RegisterUserUseCase"))            
                return Task.FromResult(_userContext.UserPermission == UserPermission.Administrator);

            if (string.Equals(useCaseName, "ListUsersUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Administrator);

            return Task.FromResult(false);
        }
    }
}
