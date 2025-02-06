using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVM.LojaVirtual.Identidade.API.Data;
using MVM.LojaVirtual.Identidade.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Data Context
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(opt => 
    opt.UseNpgsql(connectionString));

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

#region JWT
var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

AppSettings appSettings = appSettingsSection.Get<AppSettings>() ?? throw new ArgumentNullException("Não encontrado AppSettings.");
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddAuthentication(opt =>
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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();