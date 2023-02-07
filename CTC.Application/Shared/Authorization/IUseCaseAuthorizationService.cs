using CTC.Application.Shared.Request;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Authorization
{
    internal interface IUseCaseAuthorizationService
    {
        Task<bool> Authorize(string useCaseName, UserPermission userPermission);
    }
}
