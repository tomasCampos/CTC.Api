using CTC.Application.Shared.Authorization;

namespace CTC.Application.Shared.UserContext
{
    public sealed class UserContext : IUserContext, IUserContextSet
    {
        public string UserName { get; private set; } = string.Empty;

        public string UserDocument { get; private set; } = string.Empty;

        public string UserEmail { get; private set; } = string.Empty;

        public string UserPhone { get; private set; } = string.Empty;

        public UserPermission UserPermission { get; private set; } = UserPermission.Unknown;

        public void Set(string userName, string userEmail, UserPermission userPermission, string userPhone, string userDocument)
        {
            UserName = userName;
            UserDocument = userDocument;
            UserEmail = userEmail;
            UserPermission = userPermission;
            UserPhone = userPhone;
        }
    }
}
