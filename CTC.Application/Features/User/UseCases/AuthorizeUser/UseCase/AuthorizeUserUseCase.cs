using CTC.Application.Features.User.Services.Firebase;
using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase
{
    internal class AuthorizeUserUseCase : IUseCase<AuthorizeUserInput, Output>
    {
        private readonly IRequestValidator<AuthorizeUserInput> _validator;
        private readonly IFirebaseService _firebaseService;

        public AuthorizeUserUseCase(IRequestValidator<AuthorizeUserInput> validator, IFirebaseService firebaseService)
        {
            _validator = validator;
            _firebaseService = firebaseService;
        }

        public async Task<Output> Execute(AuthorizeUserInput input)
        {
            var validationResult = await _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var (sucess, token) = await _firebaseService.LoginInFireBase(input.UserEmail!, input.UserPassword!);

            if (!sucess)
                return Output.CreateInvalidParametersResult("Email e/ou senha inválidos");

            return Output.CreateOkResult(new { BearerToken = token });
        }
    }
}
