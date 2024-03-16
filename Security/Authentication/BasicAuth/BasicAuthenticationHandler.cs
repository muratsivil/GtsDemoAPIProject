using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Security.Authentication.BasicAuth;

public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authorization"))
        {
            return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Header"));
        }
        var authHeader = Request.Headers["Authorization"].ToString();
        if(!authHeader.StartsWith("Basic", StringComparison.OrdinalIgnoreCase))
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header: header is not Basic"));
        }
        var authBase64Decoded = Encoding.UTF8.GetString(
                                Convert.FromBase64String(
                                    authHeader.Replace("Basic ", "", StringComparison.OrdinalIgnoreCase)));
        var authSplit = authBase64Decoded.Split([':'], 2);

        if (authSplit.Length != 2)
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization Header: format is not valid"));
        }

        var uid = authSplit[0];
        var pwd = authSplit[1];

        if (uid != "admin" || pwd != "admin")
        {
            return Task.FromResult(AuthenticateResult.Fail("Invalid username or password"));
        }

        var client = new BasicAuthenticationClient
        {
            Name = uid,
            AuthenticationType = BasicAuthenticationDefaults.AuthenticationScheme,
            IsAuthenticated = true
        };

        var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(client, new []{
            new Claim(ClaimTypes.Name, uid)
        }));

        return Task.FromResult(
            AuthenticateResult.Success(
                new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
    }
}
