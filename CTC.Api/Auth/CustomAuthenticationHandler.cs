using CTC.Api.Auth.Services;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CTC.Api.Auth
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private static readonly string BearerPrefix = "Bearer ";
        private readonly FirebaseApp _firebaseApp;
        private readonly IUserAuthorizationService _userAuthorizationService;

        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            FirebaseApp firebaseApp,
            IUserAuthorizationService userAuthorizationService) : base(options, logger, encoder, clock)
        {
            _firebaseApp = firebaseApp;
            _userAuthorizationService = userAuthorizationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            IHeaderDictionary headers = Context.Request.Headers;
            if (!headers.ContainsKey("Authorization"))
                return AuthenticateResult.NoResult();

            string bearerToken = headers["Authorization"]!;

            if (bearerToken == null || !bearerToken.StartsWith(BearerPrefix))
                return AuthenticateResult.Fail("Invalid Authorization token");

            string token = bearerToken[BearerPrefix.Length..];
            try
            {
                FirebaseToken firebaseToken = await FirebaseAuth.GetAuth(_firebaseApp).VerifyIdTokenAsync(token);
                return AuthenticateResult.Success(await GetAuthenticationTicket(firebaseToken));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        private async Task<AuthenticationTicket> GetAuthenticationTicket(FirebaseToken firebaseToken)
        {
            var claims = await ToClaims(firebaseToken.Claims);
            return new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(claims, nameof(CustomAuthenticationHandler))
            }), JwtBearerDefaults.AuthenticationScheme);
        }

        private async Task<IEnumerable<Claim>?> ToClaims(IReadOnlyDictionary<string, object> claims)
        {
            var email = claims["email"].ToString()!;
            await _userAuthorizationService.SetUserContext(email);
            
            return new List<Claim>
            {
                new Claim("id", claims["user_id"].ToString()!),
                new Claim("email", email)
            };
        }
    }
}
