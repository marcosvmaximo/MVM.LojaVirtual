using MVM.LojaVirtual.MVC.Models.Catalogo;

namespace MVM.LojaVirtual.MVC.Services.Interfaces;

public interface ICatalogoService
{
    Task<IEnumerable<ProdutoViewModel>> ListarProdutos();
    Task<ProdutoViewModel> ObterProdutoDetalhes(Guid produtoId);
}