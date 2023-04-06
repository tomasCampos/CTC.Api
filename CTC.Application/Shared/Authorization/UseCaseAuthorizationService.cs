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

        public Task<bool> Authorize(string useCaseName, string? userEmail = null)
        {
            if(_userContext.UserPermission == UserPermission.Administrator)
                return Task.FromResult(true);

            if(string.Equals(useCaseName, "RegisterCategoryUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);           

            if (string.Equals(useCaseName, "UpdateUserUseCase"))
                return Task.FromResult(_userContext.UserEmail == userEmail);

            if (string.Equals(useCaseName, "RegisterSupplierUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "UpdateSupplierUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "DeleteSupplierUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "DeleteClientUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "RegisterClientUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "UpdateClientUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);
            
            if (string.Equals(useCaseName, "UpdateCategoryUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "DeleteCategoryUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "RegisterCostCenterUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "UpdateCostCenterUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            if (string.Equals(useCaseName, "DeleteCostCenterUseCase"))
                return Task.FromResult(_userContext.UserPermission == UserPermission.Write);

            return Task.FromResult(false);
        }
    }
}
