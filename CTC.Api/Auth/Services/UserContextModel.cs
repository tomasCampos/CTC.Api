using CTC.Application.Shared.Authorization;

namespace CTC.Api.Auth.Services
{
    internal sealed class UserContextModel
    {
        public UserContextModel(string name, string email, string phone, string document, UserPermission permission)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Document = document;
            Permission = permission;
        }

        public string Name { get; }
        public string Email { get; }
        public string Phone { get; }
        public string Document { get; }
        public UserPermission Permission { get; }

        
    }
}
