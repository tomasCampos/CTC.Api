using CTC.Application.Shared.Authorization;

namespace CTC.Application.Shared.UserContext
{
    internal interface IUserContext
    {
        string UserName { get; }
        string UserDocument { get; }
        string UserEmail { get; }
        string UserPhone { get; }
        UserPermission UserPermission { get; }
    }
}
