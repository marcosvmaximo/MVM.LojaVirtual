using MVM.LojaVirtual.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add WebApp Configuration
builder.Services.AddWebAppConfiguration(builder.Configuration);

// Identity Config
builder.Services.AddIdentityConfiguration();

var app = builder.Build();

// Use WebApp Configuration
app.UseWebAppConfiguration(app.Environment);

app.Run();