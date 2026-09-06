using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;
using SantaUrsula.API.Services;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    private readonly JwtTokenService _jwtTokenService;

    public AuthController(SantaUrsulaDbContext context, JwtTokenService jwtTokenService)
    {
        _context = context;
        _jwtTokenService = jwtTokenService;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto dto)
    {
        var usuario = await _context.Usuarios
            .Include(u => u.Rol)
            .FirstOrDefaultAsync(u => u.NombreUsuario == dto.NombreUsuario);

        if (usuario == null || !usuario.Activo)
        {
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
        }

        if (string.IsNullOrEmpty(usuario.PasswordHash) || !PasswordHasher.Verify(usuario.PasswordHash, dto.Password))
        {
            return Unauthorized(new { mensaje = "Usuario o contraseña incorrectos." });
        }

        var (token, expiraEn) = _jwtTokenService.GenerarToken(usuario, usuario.Rol?.Nombre);

        usuario.UltimoAcceso = DateTime.UtcNow;
        await _context.SaveChangesAsync();

        return Ok(new LoginResponseDto
        {
            Token = token,
            ExpiraEn = expiraEn,
            UsuarioId = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            NombreCompleto = usuario.NombreCompleto,
            RolId = usuario.RolId,
            RolNombre = usuario.Rol?.Nombre
        });
    }
}
