﻿using System.Net;

namespace CTC.Application.Shared.UseCase.IO
{
    internal interface IOutput
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool Success { get; set; }
        public string? ValidationErrorMessage { get; set; }
        public object? Body { get; set; }
    }
}
