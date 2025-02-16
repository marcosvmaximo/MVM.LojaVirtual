using System.Reflection;
using Microsoft.EntityFrameworkCore;
using MVM.LojaVirtual.Catalogo.API.Models;
using MVM.LojaVirtual.Core.Data;

namespace MVM.LojaVirtual.Catalogo.API.Data;

public class CatalogoContext : DbContext, IUnitOfWork
{
    public DbSet<Produto> Produtos { get; set; }
    
    public CatalogoContext(DbContextOptions<CatalogoContext> opt) : base(opt){}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CatalogoContext).Assembly);
    }

    public async Task<bool> Commit()
    {
        return await SaveChangesAsync() > 0;
    }
}