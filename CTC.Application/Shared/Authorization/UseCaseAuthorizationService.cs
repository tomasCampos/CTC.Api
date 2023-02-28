using System.Threading.Tasks;

namespace CTC.Application.Shared.Authorization
{
    internal sealed class UseCaseAuthorizationService : IUseCaseAuthorizationService
    {
        public Task<bool> Authorize(string useCaseName, UserPermission userPermission)
        {
            if(userPermission == UserPermission.Administrator)
                return Task.FromResult(true);

            if(string.Equals(useCaseName, "RegisterCategoryUseCase"))
                return Task.FromResult(userPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "GetUserUseCase"))            
                return Task.FromResult(userPermission == UserPermission.Read || userPermission == UserPermission.Write || userPermission == UserPermission.Read);            

            if (string.Equals(useCaseName, "RegisterUserUseCase"))            
                return Task.FromResult(userPermission == UserPermission.Administrator);

            if (string.Equals(useCaseName, "ListUsersUseCase"))
                return Task.FromResult(userPermission == UserPermission.Administrator);

            return Task.FromResult(false);
        }
    }
}
