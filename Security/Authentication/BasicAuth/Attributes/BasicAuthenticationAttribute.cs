using Microsoft.AspNetCore.Authorization;

namespace Security.Authentication.BasicAuth.Attributes;

public class BasicAuthenticationAttribute : AuthorizeAttribute
{
    public BasicAuthenticationAttribute()
    {
        AuthenticationSchemes = BasicAuthenticationDefaults.AuthenticationScheme;
    }
}
