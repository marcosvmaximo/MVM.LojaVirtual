using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVM.LojaVirtual.IdentityCore;

public static class CustomAuthorization
{
    public static bool ValidarClaimUsuario(HttpContext context, string claimName, string claimValue)
    {
        return context.User.Identity!.IsAuthenticated
               && context.User.Claims.Any(x => x.Type == claimName && x.Value.Contains(claimValue));
    }
}

public class ClaimsAuthorizationAttribute : TypeFilterAttribute
{
    public ClaimsAuthorizationAttribute(string claimName, string claimValue) : base(typeof(RequesitoClaimFilter))
    {
        Arguments = new[] { new Claim(claimName, claimValue) };
    }
}

public class RequesitoClaimFilter : IAuthorizationFilter
{
    private readonly Claim _claim;

    public RequesitoClaimFilter(Claim claim)
    {
        _claim = claim;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (!context.HttpContext.User.Identity!.IsAuthenticated)
        {
            context.Result = new StatusCodeResult(401);
            return;
        }

        if (!CustomAuthorization.ValidarClaimUsuario(context.HttpContext, _claim.Type, _claim.Value))
        {
            context.Result = new StatusCodeResult(403);
        }
    }
}