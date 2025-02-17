using MVM.LojaVirtual.Core.Domain;

namespace MVM.LojaVirtual.Catalogo.API.Models;

public class Produto : Entity, IAggregateRoot
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Valor { get; set; }
    public int QuantidadeEstoque { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public string Imagem { get; set; }
}