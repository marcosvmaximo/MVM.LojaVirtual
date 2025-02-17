using System.ComponentModel.DataAnnotations;

namespace MVM.LojaVirtual.Identidade.API.Models;

public class UsuarioRegistoViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    public string Senha { get; set; }
    
    [Compare("Senha", ErrorMessage = "As senhas não conferem.")]
    public string ConfirmarSenha { get; set; }
}

public class UsuarioLoginViewModel
{
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [EmailAddress(ErrorMessage = "O campo {0} está em um formato inválido.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "O campo {0} é obrigatório.")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.")]
    public string Senha { get; set; }
}