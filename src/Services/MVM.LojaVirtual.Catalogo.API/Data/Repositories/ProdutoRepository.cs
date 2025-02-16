using Microsoft.EntityFrameworkCore;
using MVM.LojaVirtual.Catalogo.API.Models;
using MVM.LojaVirtual.Core.Data;

namespace MVM.LojaVirtual.Catalogo.API.Data.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly CatalogoContext _context;

    public ProdutoRepository(CatalogoContext context)
    {
        _context = context;
    }
    
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Produto?>> ObterTodos()
    {
        return await _context.Produtos.AsNoTracking().ToListAsync();
    }

    public async Task<Produto?> ObterPorId(Guid id)
    {
        return await _context.Produtos.FindAsync(id);
    }

    public async Task Adicionar(Produto produto)
    {
        await _context.AddAsync(produto);
    }

    public async Task Atualizar(Produto produto)
    { 
        _context.Update(produto);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}