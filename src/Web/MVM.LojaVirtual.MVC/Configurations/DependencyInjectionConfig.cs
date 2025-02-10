using MVM.LojaVirtual.MVC.Services;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Configurations;

public static class DependencyInjectionConfig
{
    public static void AddRegisterServices(this IServiceCollection services)
    {
        services.AddHttpClient<IAuthService, AuthService>();
        services.AddHttpContextAccessor();
        services.AddScoped<IUserService, UserService>();
    }
}