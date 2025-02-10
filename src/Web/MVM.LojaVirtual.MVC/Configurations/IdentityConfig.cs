using Microsoft.AspNetCore.Authentication.Cookies;

namespace MVM.LojaVirtual.MVC.Configurations;

public static class IdentityConfig
{
    public static void AddIdentityConfiguration(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(opt =>
            {
                opt.LoginPath = "/login";
                opt.AccessDeniedPath = "/acesso-negado";
                opt.LogoutPath = "/home";
            });
    }

    public static void UseIdentityConfiguration(this IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseAuthorization();
    }
}