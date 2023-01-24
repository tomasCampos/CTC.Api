using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CTC.Api.Authentication
{
    public class CustomAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private static readonly string BearerPrefix = "Bearer ";
        private readonly FirebaseApp _firebaseApp;

        public CustomAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            FirebaseApp firebaseApp) : base(options, logger, encoder, clock)
        {
            _firebaseApp = firebaseApp;
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
                return AuthenticateResult.Success(GetAuthenticationTicket(firebaseToken));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        private static AuthenticationTicket GetAuthenticationTicket(FirebaseToken firebaseToken)
        {
            return new AuthenticationTicket(new ClaimsPrincipal(new List<ClaimsIdentity>
            {
                new ClaimsIdentity(ToClaims(firebaseToken.Claims), nameof(CustomAuthenticationHandler))
            }), JwtBearerDefaults.AuthenticationScheme);
        }

        private static IEnumerable<Claim>? ToClaims(IReadOnlyDictionary<string, object> claims)
        {
            //TODO: PEGAR O EMAIL DO CLAIMS E FAZER UM SELECT NO BANCO PARA OBTER O TIPO DE PERMISSÃO DO USUÁRIO
            return new List<Claim>
            {
                new Claim("id", claims["user_id"].ToString()!),
                new Claim("email", claims["email"].ToString()!)
            };
        }
    }
}
