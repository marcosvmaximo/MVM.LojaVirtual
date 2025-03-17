using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVM.LojaVirtual.MVC.Controllers.Common;
using MVM.LojaVirtual.MVC.Models;
using MVM.LojaVirtual.MVC.Models.Auth;
using MVM.LojaVirtual.MVC.Models.AuthExternal;
using MVM.LojaVirtual.MVC.Services;
using MVM.LojaVirtual.MVC.Services.Interfaces;

namespace MVM.LojaVirtual.MVC.Controllers;

public class AuthController : MainController
{
    private readonly IAuthService _service;

    public AuthController(IAuthService service)
    {
        _service = service;
    }

    [HttpGet("registrar")]
    public IActionResult Registrar()
    {
        return View();
    }

    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar(UsuarioRegistoViewModel request)
    {
        if (!ModelState.IsValid) return View(request);

        // Consumir API de identidade
        var result = await _service.Registrar(request);
        if (ResponsePossuiErros(result.ErrorResponse))
        {
            return View(request);
        }
        // Sucesso
        await RealizarLogin(result);
        
        return RedirectToAction("Index", "Home");
    }

    [HttpGet("login")]
    public IActionResult Login(string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login(UsuarioLoginViewModel request, string? returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (!ModelState.IsValid) return View(request);

        // Consumir API de identidade
        var result = await _service.Login(request);
        if (ResponsePossuiErros(result.ErrorResponse))
        {
            return View();
        }
        // Sucesso
        await RealizarLogin(result);

        if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", "Home");

        return LocalRedirect(returnUrl);
    }

    [HttpGet("logout")]
    public async Task<ActionResult> Logout()
    {
        await RealizarLogout();
        return RedirectToAction("Index", "Home");
    }

    private async Task RealizarLogout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }

    private async Task RealizarLogin(AuthenticationResponseViewModel response)
    {
        // Configs
        var jwtSecurityToken = new JwtSecurityTokenHandler().ReadToken(response.AcessToken) 
            as JwtSecurityToken ?? throw new Exception("Erro ao obter JwtSecurityToken");
        
        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = DateTimeOffset.Now.AddMinutes(60),
            IsPersistent = true
        };
        
        // Adicionar claims
        List<Claim> claims = new();
        claims.Add(new Claim("JWT", response.AcessToken));
        claims.AddRange(jwtSecurityToken.Claims);

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        
        // Log in
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);
    }  
}