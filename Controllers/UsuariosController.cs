using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;
using SantaUrsula.API.Services;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuariosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public UsuariosController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioDto>>> Get()
    {
        var items = await _context.Usuarios
            .AsNoTracking()
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                NombreCompleto = u.NombreCompleto,
                RolId = u.RolId,
                RolNombre = u.Rol.Nombre,
                Email = u.Email,
                Activo = u.Activo,
                FechaCreacion = u.FechaCreacion,
                UltimoAcceso = u.UltimoAcceso
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioDto>> Get(int id)
    {
        var item = await _context.Usuarios
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Select(u => new UsuarioDto
            {
                Id = u.Id,
                NombreUsuario = u.NombreUsuario,
                NombreCompleto = u.NombreCompleto,
                RolId = u.RolId,
                RolNombre = u.Rol.Nombre,
                Email = u.Email,
                Activo = u.Activo,
                FechaCreacion = u.FechaCreacion,
                UltimoAcceso = u.UltimoAcceso
            })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Usuario no encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<UsuarioDto>> Crear([FromBody] CrearUsuarioDto dto)
    {
        var rolExiste = await _context.Roles.AnyAsync(r => r.Id == dto.RolId);
        if (!rolExiste) return BadRequest(new { mensaje = "Rol no encontrado." });

        var usuario = new Data.Models.Usuario
        {
            NombreUsuario = dto.NombreUsuario.Trim(),
            NombreCompleto = dto.NombreCompleto.Trim(),
            RolId = dto.RolId,
            Email = dto.Email?.Trim(),
            Activo = dto.Activo,
            FechaCreacion = DateTime.UtcNow,
            PasswordHash = string.Empty // No aceptar PasswordHash; handle separately
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();

        var created = new UsuarioDto
        {
            Id = usuario.Id,
            NombreUsuario = usuario.NombreUsuario,
            NombreCompleto = usuario.NombreCompleto,
            RolId = usuario.RolId,
            RolNombre = (await _context.Roles.FindAsync(usuario.RolId))?.Nombre,
            Email = usuario.Email,
            Activo = usuario.Activo,
            FechaCreacion = usuario.FechaCreacion
        };

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarUsuarioDto dto)
    {
        var user = await _context.Usuarios.FindAsync(id);
        if (user == null) return NotFound(new { mensaje = "Usuario no encontrado." });

        if (dto.NombreUsuario != null) user.NombreUsuario = dto.NombreUsuario.Trim();
        if (dto.NombreCompleto != null) user.NombreCompleto = dto.NombreCompleto.Trim();
        if (dto.RolId.HasValue)
        {
            var rolExiste = await _context.Roles.AnyAsync(r => r.Id == dto.RolId.Value);
            if (!rolExiste) return BadRequest(new { mensaje = "Rol no encontrado." });
            user.RolId = dto.RolId.Value;
        }
        if (dto.Email != null) user.Email = dto.Email.Trim();
        if (dto.Activo.HasValue) user.Activo = dto.Activo.Value;
        if (dto.UltimoAcceso.HasValue) user.UltimoAcceso = dto.UltimoAcceso.Value;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _context.Usuarios.FindAsync(id);
        if (user == null) return NotFound(new { mensaje = "Usuario no encontrado." });
        _context.Usuarios.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpPost("{id:int}/password")]
    public async Task<IActionResult> SetPassword(int id, [FromBody] SetPasswordDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = await _context.Usuarios.FindAsync(id);
        if (user == null) return NotFound(new { mensaje = "Usuario no encontrado." });

        // Hash and store password
        user.PasswordHash = PasswordHasher.HashPassword(dto.Password);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
