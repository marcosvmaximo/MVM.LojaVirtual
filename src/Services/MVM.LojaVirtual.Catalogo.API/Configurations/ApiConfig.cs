using Microsoft.EntityFrameworkCore;
using MVM.LojaVirtual.Catalogo.API.Data;
using MVM.LojaVirtual.IdentityCore;

namespace MVM.LojaVirtual.Catalogo.API.Configurations;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        // Db Config
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CatalogoContext>(opt =>
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        
        // Identity Config
        services.AddJwtConfiguration(configuration);
        
        // Swagger Config
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        // Dependency Config
        services.AddDependencyConfiguration();
        
        // Api Config
        services.AddControllers();

        services.AddCors(opt =>
        {
            opt.AddPolicy("Total", builder => 
                builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
        });
        
        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder builder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            builder.UseDeveloperExceptionPage();
            builder.UseSwagger();
            builder.UseSwaggerUI();
        }
        
        builder.UseHttpsRedirection();
        
        builder.UseCors("Total");
        
        builder.UseRouting();
        
        builder.UseAuthentication();
        builder.UseAuthorization();
        
        builder.UseEndpoints(opt => opt.MapControllers());
        
        return builder;
    }
}