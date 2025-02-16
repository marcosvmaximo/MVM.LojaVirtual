using MVM.LojaVirtual.Catalogo.API.Models;
using MVM.LojaVirtual.Core.Data;

namespace MVM.LojaVirtual.Catalogo.API.Data.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IEnumerable<Produto?>> ObterTodos();
    Task<Produto?> ObterPorId(Guid id);
    Task Adicionar(Produto produto);
    Task Atualizar(Produto produto);
}