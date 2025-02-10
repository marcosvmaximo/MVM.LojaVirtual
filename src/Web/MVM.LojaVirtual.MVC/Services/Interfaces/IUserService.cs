using System.Security.Claims;

namespace MVM.LojaVirtual.MVC.Services.Interfaces;

public interface IUserService
{
    string Nome { get; }
    Guid ObterUserId();
    string ObterEmail();
    string ObterToken();
    bool EstaAutenticado();
    bool PossuiRole(string role);
    IEnumerable<Claim> ObterClaims();
    HttpContext? ObterHttpContext();
}