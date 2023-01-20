﻿using CTC.Application.Features.User.Models;
using CTC.Application.Features.User.RegisterUser.Repositories;
using CTC.Application.Features.User.RegisterUser.UseCase.IO;
using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using System.Net;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.RegisterUser.UseCase
{
    internal sealed class RegisterUserUseCase : IUseCase<RegisterUserInput, RegisterUserOutput>
    {
        private readonly IRequestValidator<RegisterUserInput> _validator;
        private readonly IRegisterUserRepository _repository;

        public RegisterUserUseCase(IRequestValidator<RegisterUserInput> validator, IRegisterUserRepository repository)
        {
            _validator = validator;
            _repository = repository;
        }

        public async Task<RegisterUserOutput> Execute(RegisterUserInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
            {
                return new RegisterUserOutput
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    ValidationErrorMessage = validationResult.ErrorMessage
                };
            }

            var user = new UserModel(input.UserFirstName!, input.UserEmail!, input.UserPhone!, input.UserDocument!, input.UserLastName!, (int)input.UserPermission!, input.UserPassword!);
            var success = await _repository.InsertUser(user);

            if (success) 
            {
                //TODO: cadastrar usuário no firebase
                return new RegisterUserOutput
                {
                    StatusCode = HttpStatusCode.Created
                };
            }

            return new RegisterUserOutput
            {
                StatusCode = HttpStatusCode.InternalServerError,
                ValidationErrorMessage = "Não foi possível cadastrar o usuário. Tente novamente mais tarde."
            };
        }
    }
}
