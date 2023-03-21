using Firebase.Auth;
using System.Configuration;
using System;
using System.Threading.Tasks;
using FirebaseAdmin.Auth;

namespace CTC.Application.Features.User.Services.Firebase
{
    internal sealed class FirebaseService : IFirebaseService
    {
        private readonly FirebaseAuthProvider _authProvider;
        private readonly FirebaseAdmin.Auth.FirebaseAuth _firebaseAdmin;

        private const string FireBaseApiKeyEnvironmentVariableName = "FIRE_BASE_API_KEY";

        public FirebaseService()
        {
            var fireBaseApiKey = Environment.GetEnvironmentVariable(FireBaseApiKeyEnvironmentVariableName)
                ?? throw new ConfigurationErrorsException($"Missing environment variable named {FireBaseApiKeyEnvironmentVariableName}");

            _firebaseAdmin = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
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

        public async Task<bool> DeleteFireBaseUser(string userEmail)
        {
            try
            {
                var userRecord = await _firebaseAdmin.GetUserByEmailAsync(userEmail);
                await _firebaseAdmin.DeleteUserAsync(userRecord.Uid);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task RegisterFireBaseUser(string userPassword, string userEmail, string userDisplayName)
        {
            var args = new UserRecordArgs()
            {
                Email = userEmail,
                Password = userPassword,
                DisplayName = userDisplayName
            };

            _ = await _firebaseAdmin.CreateUserAsync(args);
        }

        public async Task UpdateFireBaseUser(UserRecordArgs userArgs)
        {
            _ = await _firebaseAdmin.UpdateUserAsync(userArgs);
        }

        public async Task<UserRecord> GetFirebaseUserByEmail(string userEmail)
        {
            return await _firebaseAdmin.GetUserByEmailAsync(userEmail);
        }
    }
}
