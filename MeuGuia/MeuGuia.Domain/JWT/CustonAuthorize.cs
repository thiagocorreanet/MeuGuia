using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MeuGuia.Domain.JWT;

public class CustonAuthorize
{
    public static bool ValideteClaimsUser(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity.IsAuthenticated && context.User.Claims.Any(x => x.Type == claimName && x.Value.Contains(claimValue));
    }
}

public class ClaimsAuthorizeAttribute : Attribute, IAuthorizationFilter
{
    private readonly string _claimName;
    private readonly string _claimValue;

    public ClaimsAuthorizeAttribute(string claimName, string claimValue)
    {
        _claimName = claimName;
        _claimValue = claimValue;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustonAuthorize.ValideteClaimsUser(context.HttpContext, _claimName, _claimValue))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}
