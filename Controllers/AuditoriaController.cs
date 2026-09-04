using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuditoriaController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public AuditoriaController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuditoriaDto>>> Get()
    {
        var items = await _context.Auditoria.AsNoTracking().Select(a => new AuditoriaDto
        {
            Id = a.Id,
            UsuarioId = a.UsuarioId,
            UsuarioNombre = a.Usuario != null ? a.Usuario.NombreCompleto : null,
            FechaHora = a.FechaHora,
            Accion = a.Accion,
            TablaAfectada = a.TablaAfectada,
            RegistroAfectadoId = a.RegistroAfectadoId,
            ValorAnterior = a.ValorAnterior,
            ValorNuevo = a.ValorNuevo,
            DireccionIP = a.DireccionIP
        }).ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearAuditoriaDto dto)
    {
        if (dto.UsuarioId.HasValue)
        {
            var uexists = await _context.Usuarios.AnyAsync(u => u.Id == dto.UsuarioId.Value);
            if (!uexists) return BadRequest(new { mensaje = "Usuario no encontrado." });
        }

        var ent = new Data.Models.Auditorium
        {
            UsuarioId = dto.UsuarioId,
            FechaHora = DateTime.Now,
            Accion = dto.Accion,
            TablaAfectada = dto.TablaAfectada,
            RegistroAfectadoId = dto.RegistroAfectadoId,
            ValorAnterior = dto.ValorAnterior,
            ValorNuevo = dto.ValorNuevo,
            DireccionIP = dto.DireccionIP
        };

        _context.Auditoria.Add(ent);
        await _context.SaveChangesAsync();

        var created = new AuditoriaDto
        {
            Id = ent.Id,
            UsuarioId = ent.UsuarioId,
            UsuarioNombre = ent.UsuarioId.HasValue ? (await _context.Usuarios.FindAsync(ent.UsuarioId.Value))?.NombreCompleto : null,
            FechaHora = ent.FechaHora,
            Accion = ent.Accion,
            TablaAfectada = ent.TablaAfectada,
            RegistroAfectadoId = ent.RegistroAfectadoId,
            ValorAnterior = ent.ValorAnterior,
            ValorNuevo = ent.ValorNuevo,
            DireccionIP = ent.DireccionIP
        };

        return CreatedAtAction(nameof(Get), new { id = ent.Id }, created);
    }
}