using MVM.LojaVirtual.MVC.Configurations.Extensions;
using MVM.LojaVirtual.MVC.Services;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Configurations;

public static class DependencyInjectionConfig
{
    public static void AddRegisterServices(this IServiceCollection services)
    {
        services.AddTransient<HttpClientHandlerCustomDelegating>();
        
        services.AddHttpClient<IAuthService, AuthService>();
        services.AddHttpClient<ICatalogoService, CatalogoService>()
            .AddHttpMessageHandler<HttpClientHandlerCustomDelegating>();
        
        services.AddHttpContextAccessor();
        
        services.AddScoped<IUserService, UserService>();
    }
}