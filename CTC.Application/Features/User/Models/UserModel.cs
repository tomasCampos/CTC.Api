using CTC.Application.Shared;
using System;

namespace CTC.Application.Features.User.Models
{
    internal sealed class UserModel : PersonModel
    {
        public UserModel(in string personId, in string firstName, in string email, in string phone, in string document, in string userId, in string lastName, in int permission, in string password)
            : base(personId, firstName, email, phone, document)
        {
            if(string.IsNullOrWhiteSpace(userId))
                throw new ArgumentNullException(nameof(userId));
            if(string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));
            if(!Enum.IsDefined(typeof(UserPermission), permission))
                throw new ArgumentOutOfRangeException(nameof(permission));
            if(string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            UserId = userId;
            LastName = lastName;
            Permission = permission;
            Password = password;
        }

        public UserModel(in string firstName, in string email, in string phone, in string document, in string lastName, in int permission, in string password)
            : base(Guid.NewGuid().ToString(), firstName, email, phone, document)
        {
            if (string.IsNullOrWhiteSpace(lastName))
                throw new ArgumentNullException(nameof(lastName));
            if (!Enum.IsDefined(typeof(UserPermission), permission))
                throw new ArgumentOutOfRangeException(nameof(permission));
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password));

            UserId = Guid.NewGuid().ToString();
            LastName = lastName;
            Permission = permission;
            Password = password;
        }

        public string UserId { get; set; }
        public string LastName { get; set; }
        public int Permission { get; set; }
        public string Password { get; set; }
    }
}