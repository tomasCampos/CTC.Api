using CTC.Application.Shared.Request;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Firebase.Auth;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase
{
    internal class AuthorizeUserUseCase : IUseCase<AuthorizeUserInput, Output>
    {
        private readonly IRequestValidator<AuthorizeUserInput> _validator;
        private readonly string FireBaseApiKey;

        public AuthorizeUserUseCase(IConfiguration configuration, IRequestValidator<AuthorizeUserInput> validator)
        {
            _validator = validator;
            FireBaseApiKey = configuration["FireBaseApiKey"]!;
        }

        public async Task<Output> Execute(AuthorizeUserInput input)
        {
            var validationResult = _validator.Validate(input);
            if (!validationResult.IsValid)
                return Output.CreateInvalidParametersResult(validationResult.ErrorMessage);

            var (sucess, token) = await LoginInFireBase(input);

            if (!sucess)
                return Output.CreateInvalidParametersResult("Email e/ou senha inválidos");

            return Output.CreateOkResult(new { BearerToken = token });
        }

        private async Task<(bool sucess, string? token)> LoginInFireBase(AuthorizeUserInput user)
        {
            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(FireBaseApiKey));

            try
            {
                FirebaseAuthLink firebaseAuthLink = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(user.UserEmail, user.UserPassword);
                return (true, firebaseAuthLink.FirebaseToken);
            }
            catch
            {
                return (false, null);
            }
        }
    }
}
