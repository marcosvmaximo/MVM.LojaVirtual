using System.ComponentModel.DataAnnotations;

namespace MVM.LojaVirtual.MVC.Models.Auth;

public class UsuarioLoginViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    public string Senha { get; set; }
}