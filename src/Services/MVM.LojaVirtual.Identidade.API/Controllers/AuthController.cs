using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MVM.LojaVirtual.Identidade.API.Models;
using MVM.LojaVirtual.IdentityCore;

namespace MVM.LojaVirtual.Identidade.API.Controllers;

[Route("api/[controller]")]
public class AuthController : MainController
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IdentityAppSettings _appSettings;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IOptions<IdentityAppSettings> appSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _appSettings = appSettings.Value;
    }
    
    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar([FromBody] UsuarioRegistoViewModel request)
    {
        if (!ModelState.IsValid) return CustomResponse();

        var user = new IdentityUser
        {
            UserName = request.Email,
            Email = request.Email,
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(user, request.Senha);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                AddError(error.Description);
            }
            return CustomResponse();
        }
        
        return CustomResponse(await GerarToken(request.Email));
    }

    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UsuarioLoginViewModel request)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(request.Email, request.Senha, false, true);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
            {
                AddError("Usuário bloqueado temporariamente.");
                return CustomResponse();
            }

            if (result.IsNotAllowed)
            {
                AddError("Usuário sem permissão");
                return CustomResponse();
            }
            
            AddError("Usuário ou senha incorretos.");
            return CustomResponse();
        }

        return CustomResponse(await GerarToken(request.Email));
    }

    private async Task<UsuarioResponse> GerarToken(string email)
    {
        var user = await _userManager.FindByEmailAsync(email) 
                   ?? throw new Exception("Usuário não encontrado ao gerar o token.");
        var claims = await _userManager.GetClaimsAsync(user);
        var roles = await _userManager.GetRolesAsync(user);
        
        claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
        claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty));
        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.Now).ToString()));
        claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64));

        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
        }

        var identityClaims = new ClaimsIdentity();
        identityClaims.AddClaims(claims);

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

        
        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Issuer,
            Audience = _appSettings.Audience,
            Subject = identityClaims,
            Expires = DateTime.UtcNow.AddHours(_appSettings.ExpiresIn),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encodedToken = tokenHandler.WriteToken(token);

        var response = new UsuarioResponse
        {
            AcessToken = encodedToken,
            ExpiresIn = TimeSpan.FromHours(_appSettings.ExpiresIn).TotalSeconds,
            UsuarioToken = new UsuarioToken
            {
                Id = user.Id,
                Email = user.Email ?? string.Empty,
                Claims = claims.Select(c => new UsuarioClaim { Type = c.Type, Value = c.Value})
            }
        };

        return response;
    }

    private static long ToUnixEpochDate(DateTime date) =>
        (long)Math.Round((date.ToUniversalTime() - new DateTime(1970, 1, 1)).TotalSeconds);
}