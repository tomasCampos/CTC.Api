﻿using System.Threading.Tasks;

namespace CTC.Application.Shared.Authorization
{
    internal interface IUseCaseAuthorizationService
    {
        Task<bool> Authorize(string useCaseName, string? userEmail = null);
    }
}
