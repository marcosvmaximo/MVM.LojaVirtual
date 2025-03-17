using Microsoft.Extensions.Options;
using MVM.LojaVirtual.MVC.Configurations.Extensions;
using MVM.LojaVirtual.MVC.Models.Auth;
using MVM.LojaVirtual.MVC.Models.AuthExternal;
using MVM.LojaVirtual.MVC.Services.Common;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Services;

public class AuthService : Service, IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.AutenticacaoUrl);
        _httpClient = httpClient;
    }
    public async Task<AuthenticationResponseViewModel> Login(UsuarioLoginViewModel usuario)
    {
        // Precisa serializar em Json, para enviar no corpo da requisição (como string mesmo)
        var content = SerializarObjeto(usuario);
        var responseHttp = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/login", content);
        
        if (!TratarErrorRequest(responseHttp))
        {
            var error = DeserializarObjeto<ErrorResponse>(await responseHttp.Content.ReadAsStringAsync());
            return new AuthenticationResponseViewModel { ErrorResponse = error! };
        }
        
        return DeserializarObjeto<AuthenticationResponseViewModel>(await responseHttp.Content.ReadAsStringAsync());
    }

    public async Task<AuthenticationResponseViewModel> Registrar(UsuarioRegistoViewModel usuario)
    {
        // Precisa serializar em Json, para enviar no corpo da requisição (como string mesmo)
        var content = SerializarObjeto(usuario);
        var responseHttp = await _httpClient.PostAsync($"{_httpClient.BaseAddress}/registrar", content);
        
        if (!TratarErrorRequest(responseHttp))
        {
            var error = DeserializarObjeto<ErrorResponse>(await responseHttp.Content.ReadAsStringAsync());
            return new AuthenticationResponseViewModel { ErrorResponse = error! };
        }
        
        return DeserializarObjeto<AuthenticationResponseViewModel>(await responseHttp.Content.ReadAsStringAsync());
    }
}