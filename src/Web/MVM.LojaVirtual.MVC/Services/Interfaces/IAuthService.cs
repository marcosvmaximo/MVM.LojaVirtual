using MVM.LojaVirtual.MVC.Models.Auth;
using MVM.LojaVirtual.MVC.Models.AuthExternal;

namespace MVM.LojaVirtual.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResponseViewModel> Login(UsuarioLogin usuario);
    Task<AuthenticationResponseViewModel> Registrar(UsuarioRegisto usuario);
}