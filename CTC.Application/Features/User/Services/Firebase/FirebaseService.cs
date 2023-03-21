using Firebase.Auth;
using System.Configuration;
using System;
using System.Threading.Tasks;

namespace CTC.Application.Features.User.Services.Firebase
{
    internal sealed class FirebaseService : IFirebaseService
    {
        private readonly FirebaseAuthProvider _authProvider;

        private const string FireBaseApiKeyEnvironmentVariableName = "FIRE_BASE_API_KEY";

        public FirebaseService()
        {
            var fireBaseApiKey = Environment.GetEnvironmentVariable(FireBaseApiKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {FireBaseApiKeyEnvironmentVariableName}");

            _authProvider = new FirebaseAuthProvider(new FirebaseConfig(fireBaseApiKey));
        }

        public async Task<(bool sucess, string? token)> LoginInFireBase(string userEmail, string userPassword)
        {
            try
            {
                FirebaseAuthLink firebaseAuthLink = await _authProvider.SignInWithEmailAndPasswordAsync(userEmail, userPassword);
                return (true, firebaseAuthLink.FirebaseToken);
            }
            catch
            {
                return (false, null);
            }
        }
    }
}
