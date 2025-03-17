using System.Net.Http.Headers;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Configurations.Extensions;

public class HttpClientHandlerCustomDelegating : DelegatingHandler
{
    private readonly IUserService _user;

    public HttpClientHandlerCustomDelegating(IUserService user)
    
    {
        _user = user;
    }
    
    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authorizationHeader = _user.ObterHttpContext()!.Request.Headers["Authorization"];
        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string?>() {authorizationHeader });
        }
        
        var token = _user.ObterToken();
        if (!string.IsNullOrEmpty(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
        
        return base.SendAsync(request, cancellationToken);
    }
}