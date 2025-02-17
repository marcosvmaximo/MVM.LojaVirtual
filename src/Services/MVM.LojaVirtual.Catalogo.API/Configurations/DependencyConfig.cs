using MVM.LojaVirtual.Catalogo.API.Data.Repositories;

namespace MVM.LojaVirtual.Catalogo.API.Configurations;

public static class DependencyConfig
{
    public static IServiceCollection AddDependencyConfiguration(this IServiceCollection services)
    {
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        return services;
    }
}