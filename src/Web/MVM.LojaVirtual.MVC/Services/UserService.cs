using System.Security.Claims;
using MVM.LojaVirtual.MVC.Services.Common;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Services;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _acessor;

    public UserService(IHttpContextAccessor acessor)
    {
        _acessor = acessor;
    }

    public string? Nome => _acessor.HttpContext?.User?.Identity?.Name;
    
    public Guid ObterUserId()
    {
        var userIdClaim = _acessor.HttpContext!.User.FindFirstValue("sub");
        return userIdClaim != null ? Guid.Parse(userIdClaim) : Guid.Empty;
    }

    public string ObterEmail()
    {
        return _acessor.HttpContext!.User.FindFirstValue("email") ?? string.Empty;
    }

    public string ObterToken()
    {
        return _acessor.HttpContext!.User.FindFirstValue("JWT") ?? string.Empty;
    }

    public bool EstaAutenticado()
    {
        return _acessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    }

    public bool PossuiRole(string role)
    {
        return _acessor.HttpContext?.User.IsInRole(role) ?? false;
    }

    public IEnumerable<Claim> ObterClaims()
    {
        return _acessor.HttpContext?.User.Claims ?? new List<Claim>();
    }

    public HttpContext? ObterHttpContext()
    {
        return _acessor.HttpContext;
    }
}