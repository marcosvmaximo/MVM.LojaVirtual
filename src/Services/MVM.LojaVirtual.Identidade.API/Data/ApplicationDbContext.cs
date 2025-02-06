using Microsoft.EntityFrameworkCore;

namespace MVM.LojaVirtual.Identidade.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt)
    {
        
    }
}