using Microsoft.Extensions.Options;
using MVM.LojaVirtual.MVC.Configurations.Extensions;
using MVM.LojaVirtual.MVC.Models.Catalogo;
using MVM.LojaVirtual.MVC.Services.Common;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Services;

public class CatalogoService : Service, ICatalogoService
{
    private readonly HttpClient _httpClient;

    public CatalogoService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
        httpClient.BaseAddress = new Uri(settings.Value.CatalogoUrl);
        _httpClient = httpClient;
    }
    public async Task<IEnumerable<ProdutoViewModel>> ListarProdutos()
    {
        // GET para /produtos
        var responseHttp = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/produtos");
        
        if (!TratarErrorRequest(responseHttp))
        {
            // Tratar Erro
        }
        
        return DeserializarObjeto<IEnumerable<ProdutoViewModel>>(await responseHttp.Content.ReadAsStringAsync());
    }

    public async Task<ProdutoViewModel> ObterProdutoDetalhes(Guid produtoId)
    {
        // GET para /produtos/{id}
        var responseHttp = await _httpClient.GetAsync($"{_httpClient.BaseAddress}/produtos/{produtoId}");
        
        if (!TratarErrorRequest(responseHttp))
        {
            // Tratar Erro
        }
        
        return DeserializarObjeto<ProdutoViewModel>(await responseHttp.Content.ReadAsStringAsync());
    }
}