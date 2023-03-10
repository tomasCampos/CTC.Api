using CTC.Application.Shared.Authorization;

namespace CTC.Application.Shared.UserContext
{
    public interface IUserContextSet
    {
        void Set(string userName, string userEmail, UserPermission userPermission, string userPhone, string userDocument, string firebaseUserIdToken);
    }
}
