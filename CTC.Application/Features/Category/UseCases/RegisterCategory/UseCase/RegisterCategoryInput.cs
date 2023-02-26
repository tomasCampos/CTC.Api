﻿using CTC.Application.Shared.Authorization;
using CTC.Application.Shared.UseCase.IO;

namespace CTC.Application.Features.Category.UseCases.RegisterCategory.UseCase
{
    public sealed class RegisterCategoryInput : IInput
    {
        public RegisterCategoryInput(string? categoryName, UserPermission requestUserPermission)
        {
            RequestUserPermission = requestUserPermission;
            CategoryName = categoryName;
        }

        public string? CategoryName { get; set; }
        public UserPermission RequestUserPermission { get; }
    }
}
