﻿using CTC.Application.Shared.Authorization;

namespace CTC.Api.Controllers.User.Contracts
{
    public sealed class UpdateUserRequest
    {
        public string? UserId { get; set; }
        public string? UserFirstName { get; set; }
        public string? UserEmail { get; set; }
        public string? UserPhone { get; set; }
        public string? UserDocument { get; set; }
        public string? UserLastName { get; set; }
        public UserPermission? UserPermission { get; set; }
        public string? UserPassword { get; set; }
    }
}
