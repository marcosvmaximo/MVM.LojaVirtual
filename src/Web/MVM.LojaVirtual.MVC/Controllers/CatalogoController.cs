
using Microsoft.AspNetCore.Mvc;
using MVM.LojaVirtual.MVC.Controllers.Common;
using MVM.LojaVirtual.MVC.Models.Catalogo;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Controllers;

public class CatalogoController : MainController
{
    private readonly ICatalogoService _service;

    public CatalogoController(ICatalogoService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("catalogo")]
    public async Task<IActionResult> ListarProdutos()
    {
        var produtos = await _service.ListarProdutos();
        return View(produtos);
    }
    
    [HttpGet]
    [Route("catalogo/{id:guid}")]
    public async Task<IActionResult> ProdutoDetalhe(Guid id)
    {
        var produto = await _service.ObterProdutoDetalhes(id);
        return View(produto);
    }
}