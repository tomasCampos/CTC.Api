﻿using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Shared.Request
{
    internal interface IRequestValidator<TRequest> where TRequest : IInput
    {
        Task<RequestValidationModel> Validate(TRequest request);
    }
}
