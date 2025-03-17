using MVM.LojaVirtual.MVC.Configurations.Extensions;

namespace MVM.LojaVirtual.MVC.Configurations;

public static class WebAppConfig
{
    public static IServiceCollection AddWebAppConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllersWithViews();
        services.AddRegisterServices();

        services.Configure<AppSettings>(configuration);
        
        return services;
    }

    public static IApplicationBuilder UseWebAppConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the HTTP request pipeline.
        // if (env.IsDevelopment())
        // {
        //     app.UseDeveloperExceptionPage();
        // }
        // else
        // {
        //     app.UseExceptionHandler("/error/500");
        //     app.UseStatusCodePagesWithRedirects("/error/{0}");
        //     app.UseHsts();
        // }
        app.UseExceptionHandler("/error/500");
        app.UseStatusCodePagesWithRedirects("/error/{0}");
        app.UseHsts();
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseIdentityConfiguration();

        app.UseMiddleware<ExceptionMiddleware>();
        
        app.UseEndpoints(endPoints =>
        {
            endPoints.MapControllerRoute(
                name: "home",
                pattern: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            endPoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}"
            );
        });

        return app;
    }
}