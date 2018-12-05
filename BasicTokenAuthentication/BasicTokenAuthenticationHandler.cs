using System;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BasicTokenAuthentication
{
    public class BasicTokenAuthenticationHandler : AuthenticationHandler<BasicTokenAuthenticationOptions>
    {
        private const string AuthorizationHeaderName = "Authorization";
        private const string SchemeName = "Token";
        private readonly IBasicTokenAuthenticationService _authenticationService;

        public BasicTokenAuthenticationHandler(
            IOptionsMonitor<BasicTokenAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IBasicTokenAuthenticationService authenticationService)
            : base(options, logger, encoder, clock)
        {
            _authenticationService = authenticationService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(AuthorizationHeaderName))
            {
                //Authorization header not in request
                return AuthenticateResult.Fail("Request denied code:1001");
            }

            if (!AuthenticationHeaderValue.TryParse(Request.Headers[AuthorizationHeaderName], out AuthenticationHeaderValue headerValue))
            {
                //Invalid Authorization header
                return AuthenticateResult.Fail("Request denied code:1002");
            }

            if (!string.IsNullOrEmpty(SchemeName) && !SchemeName.Equals(headerValue.Scheme, StringComparison.OrdinalIgnoreCase))
            {
                //Not Basic authentication header
                return AuthenticateResult.Fail("Request denied code:1003");
            }

            // byte[] headerValueBytes = Convert.FromBase64String(headerValue.Parameter);
            // string token = Encoding.UTF8.GetString(headerValueBytes);

            string token = headerValue.Parameter;
            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Invalid authentication header");
            }

            bool isValidUser = await _authenticationService.IsValidTokenAsync(token);

            if (!isValidUser)
            {
                return AuthenticateResult.Fail("Invalid token");
            }
            var claims = new[] { new Claim(ClaimTypes.Name, token) };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return AuthenticateResult.Success(ticket);
        }

        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            // Response.Headers["WWW-Authenticate"] = $"Basic realm=\"{Options.Realm}\", charset=\"UTF-8\"";
            var test = await HandleAuthenticateAsync();
            if(!test.Succeeded)
            {
                Response.StatusCode = 401;
            }
        }
    }

}