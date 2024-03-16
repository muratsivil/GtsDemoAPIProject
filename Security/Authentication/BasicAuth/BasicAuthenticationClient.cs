using System.Security.Principal;

namespace Security.Authentication.BasicAuth;

public class BasicAuthenticationClient : IIdentity
{
    public string? AuthenticationType { get; set; }

    public bool IsAuthenticated { get; set; }

    public string? Name { get; set; }
}
