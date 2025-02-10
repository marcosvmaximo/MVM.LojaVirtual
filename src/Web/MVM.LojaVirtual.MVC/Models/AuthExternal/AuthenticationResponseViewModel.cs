namespace MVM.LojaVirtual.MVC.Models.AuthExternal;

public class AuthenticationResponseViewModel
{
    public string AcessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UsuarioReponse UsuarioReponse { get; set; }
    public ErrorResponse ErrorResponse { get; set; }
}