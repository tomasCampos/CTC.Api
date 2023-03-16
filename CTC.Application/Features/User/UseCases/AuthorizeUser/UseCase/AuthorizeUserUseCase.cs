using CTC.Application.Shared.Request.Validator;
using CTC.Application.Shared.UseCase;
using CTC.Application.Shared.UseCase.IO;
using Firebase.Auth;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.UseCases.AuthorizeUser.UseCase
{
    internal class AuthorizeUserUseCase : IUseCase<AuthorizeUserInput, Output>
    {
        private readonly IRequestValidator<AuthorizeUserInput> _validator;

        private readonly string FireBaseApiKey;
        private const string FireBaseApiKeyEnvironmentVariableName = "FIRE_BASE_API_KEY";

        public AuthorizeUserUseCase(IRequestValidator<AuthorizeUserInput> validator)
        {
            _validator = validator;
            FireBaseApiKey = Environment.GetEnvironmentVariable(FireBaseApiKeyEnvironmentVariableName) 
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {FireBaseApiKeyEnvironmentVariableName}");
        }

        public async Task<Output> Execute(AuthorizeUserInput input)
        {
            var validationResult = await _validator.Validate(input);
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
