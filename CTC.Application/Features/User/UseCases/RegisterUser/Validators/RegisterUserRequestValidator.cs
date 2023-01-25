﻿using CTC.Application.Features.User.UseCases.RegisterUser.UseCase.IO;
using CTC.Application.Shared.Request;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CTC.Application.Features.User.UseCases.RegisterUser.Validators
{
    internal sealed class RegisterUserRequestValidator : IRequestValidator<RegisterUserInput>
    {
        const string ValidEmailRegex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
        public RequestValidationModel Validate(RegisterUserInput request)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(request.UserLastName))
                errors.Add("O último nome do usuário deve ser informado");
            if (!request.UserPermission.HasValue)
                errors.Add("A permissão do usuário deve ser informada");
            if (string.IsNullOrWhiteSpace(request.UserPassword))
                errors.Add("A senha do usuário deve ser informada");
            if (request.UserPassword!.Length > 6)
                errors.Add("A senha do usuário deve conter pelo menos 6 dígitos");
            if (string.IsNullOrWhiteSpace(request.UserFirstName))
                errors.Add("O primeiro nome do usuário deve ser informado");
            if (string.IsNullOrWhiteSpace(request.UserEmail))
                errors.Add("O E-mail do usuário deve ser informado");
            if (!Regex.IsMatch(request.UserEmail, ValidEmailRegex, RegexOptions.IgnoreCase))
                errors.Add("O E-mail do usuário não é válido");
            if (!string.IsNullOrWhiteSpace(request.UserDocument) && request.UserDocument.Length < 11)
                errors.Add("O número do documento do usuário deve conter pelo menos 11 dígitos");

            return new RequestValidationModel(errors);
        }
    }
}