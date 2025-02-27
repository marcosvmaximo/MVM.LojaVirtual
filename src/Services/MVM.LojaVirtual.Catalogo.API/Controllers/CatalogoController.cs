using Microsoft.AspNetCore.Mvc;
using MVM.LojaVirtual.Catalogo.API.Data;
using MVM.LojaVirtual.Catalogo.API.Data.Repositories;
using MVM.LojaVirtual.Catalogo.API.Models;
using MVM.LojaVirtual.IdentityCore;

namespace MVM.LojaVirtual.Catalogo.API.Controllers;

[ApiController]
public class CatalogoController : Controller
{
    private readonly IProdutoRepository _repository;

    public CatalogoController(IProdutoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet("catalogo/produtos")]
    public async Task<IEnumerable<Produto?>> Index()
    {
        return await _repository.ObterTodos();
    }
    
    [HttpGet("catalogo/produtos/{id}")]
    [ClaimsAuthorization("Catalogo", "Ler")]
    public async Task<Produto?> ProdutoDetalhe(Guid id)
    {
        return await _repository.ObterPorId(id);
    }
}