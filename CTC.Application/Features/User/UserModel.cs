using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.Models.Person;
using System;

namespace CTC.Application.Features.User
{
    internal sealed class UserModel : PersonModel
    {
        public UserModel(in string userId, in string personId, in string firstName, in string email, in string phone, in string lastName, in string password, in int permission, in string document)
            : base(personId, firstName, email, phone, document)
        {
            UserId = userId;
            LastName = lastName;
            Permission = (UserPermission)permission;
            Password = password;
        }

        public UserModel(in string firstName, in string email, in string phone, in string document, in string lastName, in int permission, in string password)
            : base(Guid.NewGuid().ToString(), firstName, email, phone, document)
        {
            UserId = Guid.NewGuid().ToString();
            LastName = lastName;
            Permission = (UserPermission)permission;
            Password = password;
        }

        public UserModel() : base()
        {
        }

        public string? UserId { get; set; }
        public string? LastName { get; set; }
        public UserPermission? Permission { get; set; }
        public string? Password { get; set; }
    }
}