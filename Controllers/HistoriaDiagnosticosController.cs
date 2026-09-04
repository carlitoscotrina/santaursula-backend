using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriaDiagnosticosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public HistoriaDiagnosticosController(SantaUrsulaDbContext context) => _context = context;
    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistoriaDiagnosticoDto>>> Get([FromQuery] int? historiaClinicaId)
    {
        var q = _context.HistoriaDiagnosticos.AsNoTracking().AsQueryable();
        if (historiaClinicaId.HasValue) q = q.Where(h => h.HistoriaClinicaId == historiaClinicaId.Value);

        var items = await q.Select(h => new HistoriaDiagnosticoDto
        {
            Id = h.Id,
            HistoriaClinicaId = h.HistoriaClinicaId,
            DiagnosticoId = h.DiagnosticoId,
            DiagnosticoCodigo = h.Diagnostico.CodigoCIE10,
            DiagnosticoNombre = h.Diagnostico.Nombre,
            Tipo = h.Tipo
        }).ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HistoriaDiagnosticoDto>> GetById(int id)
    {
        var item = await _context.HistoriaDiagnosticos.AsNoTracking().Where(h => h.Id == id).Select(h => new HistoriaDiagnosticoDto
        {
            Id = h.Id,
            HistoriaClinicaId = h.HistoriaClinicaId,
            DiagnosticoId = h.DiagnosticoId,
            DiagnosticoCodigo = h.Diagnostico.CodigoCIE10,
            DiagnosticoNombre = h.Diagnostico.Nombre,
            Tipo = h.Tipo
        }).FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Registro no encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearHistoriaDiagnosticoDto dto)
    {
        var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId);
        if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });

        var diagExiste = await _context.Diagnosticos.AnyAsync(d => d.Id == dto.DiagnosticoId);
        if (!diagExiste) return BadRequest(new { mensaje = "Diagnóstico no encontrado." });

        var duplicado = await _context.HistoriaDiagnosticos.AnyAsync(hd => hd.HistoriaClinicaId == dto.HistoriaClinicaId && hd.DiagnosticoId == dto.DiagnosticoId && hd.Tipo == dto.Tipo);
        if (duplicado) return Conflict(new { mensaje = "El diagnóstico ya está asociado a la historia clínica con ese tipo." });

        var ent = new Data.Models.HistoriaDiagnostico
        {
            HistoriaClinicaId = dto.HistoriaClinicaId,
            DiagnosticoId = dto.DiagnosticoId,
            Tipo = dto.Tipo
        };

        _context.HistoriaDiagnosticos.Add(ent);
        await _context.SaveChangesAsync();

        var created = await _context.HistoriaDiagnosticos.AsNoTracking().Where(h => h.Id == ent.Id).Select(h => new HistoriaDiagnosticoDto
        {
            Id = h.Id,
            HistoriaClinicaId = h.HistoriaClinicaId,
            DiagnosticoId = h.DiagnosticoId,
            DiagnosticoCodigo = h.Diagnostico.CodigoCIE10,
            DiagnosticoNombre = h.Diagnostico.Nombre,
            Tipo = h.Tipo
        }).FirstAsync();

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _context.HistoriaDiagnosticos.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Registro no encontrado." });
        _context.HistoriaDiagnosticos.Remove(ent);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
