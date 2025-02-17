using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MVM.LojaVirtual.IdentityCore;

public static class JwtConfig
{
    public static IServiceCollection AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        #region JWT
        var appSettingsSection = configuration.GetSection("IdentityAppSettings");
        services.Configure<IdentityAppSettings>(appSettingsSection);

        IdentityAppSettings appSettings = appSettingsSection.Get<IdentityAppSettings>() 
                                          ?? throw new ArgumentNullException($"Não encontrado o arquivo de configuração: {nameof(IdentityAppSettings)}.");
        var key = Encoding.ASCII.GetBytes(appSettings.Secret);

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(bearerOpt =>
        {
            bearerOpt.RequireHttpsMetadata = true;
            bearerOpt.SaveToken = true;
            bearerOpt.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true, // Valida quem foi o emissor do token
                ValidateAudience = true, // Valida quem é a aplicação "foco" do token
                ValidateIssuerSigningKey = true, // Valida a chave de entrada, do emissor
                IssuerSigningKey = new SymmetricSecurityKey(key), // Algoritmo para a chave
                ValidAudience = appSettings.Audience,
                ValidIssuer = appSettings.Issuer
            };
        });
        #endregion

        return services;
    }
}