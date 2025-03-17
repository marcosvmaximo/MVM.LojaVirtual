using MVM.LojaVirtual.MVC.Models.Auth;
using MVM.LojaVirtual.MVC.Models.AuthExternal;

namespace MVM.LojaVirtual.MVC.Services.Interfaces;

public interface IAuthService
{
    Task<AuthenticationResponseViewModel> Login(UsuarioLoginViewModel usuario);
    Task<AuthenticationResponseViewModel> Registrar(UsuarioRegistoViewModel usuario);
}