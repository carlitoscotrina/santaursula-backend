using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DiagnosticosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public DiagnosticosController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DiagnosticoDto>>> Get()
    {
        var items = await _context.Diagnosticos
            .AsNoTracking()
            .Select(d => new DiagnosticoDto
            {
                Id = d.Id,
                CodigoCIE10 = d.CodigoCIE10,
                Nombre = d.Nombre,
                Descripcion = d.Descripcion,
                Activo = d.Activo
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DiagnosticoDto>> Get(int id)
    {
        var item = await _context.Diagnosticos
            .AsNoTracking()
            .Where(d => d.Id == id)
            .Select(d => new DiagnosticoDto
            {
                Id = d.Id,
                CodigoCIE10 = d.CodigoCIE10,
                Nombre = d.Nombre,
                Descripcion = d.Descripcion,
                Activo = d.Activo
            })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Diagnóstico no encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<DiagnosticoDto>> Crear([FromBody] CrearDiagnosticoDto dto)
    {
        var existe = await _context.Diagnosticos.AnyAsync(d => d.CodigoCIE10 == dto.CodigoCIE10);
        if (existe) return Conflict(new { mensaje = "Ya existe un diagnóstico con ese código CIE10." });

        var ent = new Data.Models.Diagnostico
        {
            CodigoCIE10 = dto.CodigoCIE10.Trim(),
            Nombre = dto.Nombre.Trim(),
            Descripcion = dto.Descripcion?.Trim(),
            Activo = dto.Activo
        };

        _context.Diagnosticos.Add(ent);
        await _context.SaveChangesAsync();

        var created = new DiagnosticoDto
        {
            Id = ent.Id,
            CodigoCIE10 = ent.CodigoCIE10,
            Nombre = ent.Nombre,
            Descripcion = ent.Descripcion,
            Activo = ent.Activo
        };

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarDiagnosticoDto dto)
    {
        var ent = await _context.Diagnosticos.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Diagnóstico no encontrado." });

        if (!string.IsNullOrWhiteSpace(dto.CodigoCIE10))
        {
            var code = dto.CodigoCIE10.Trim();
            var existe = await _context.Diagnosticos.AnyAsync(d => d.CodigoCIE10 == code && d.Id != id);
            if (existe) return Conflict(new { mensaje = "Otro diagnóstico ya usa ese código CIE10." });
            ent.CodigoCIE10 = code;
        }

        if (!string.IsNullOrWhiteSpace(dto.Nombre)) ent.Nombre = dto.Nombre.Trim();
        if (dto.Descripcion != null) ent.Descripcion = dto.Descripcion.Trim();
        if (dto.Activo.HasValue) ent.Activo = dto.Activo.Value;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _context.Diagnosticos.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Diagnóstico no encontrado." });

        _context.Diagnosticos.Remove(ent);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
