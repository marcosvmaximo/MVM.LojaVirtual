namespace MVM.LojaVirtual.MVC.Models.AuthExternal;

public class UsuarioReponse
{
    public string Id { get; set; }
    public string Email { get; set; }
    public IEnumerable<UsuarioClaim> Claims { get; set; }
}