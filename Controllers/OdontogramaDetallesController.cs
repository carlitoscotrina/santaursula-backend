using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OdontogramaDetallesController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public OdontogramaDetallesController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OdontogramaDetalleDto>>> Get()
    {
        var items = await _context.OdontogramaDetalles
            .AsNoTracking()
            .Select(d => new OdontogramaDetalleDto
            {
                Id = d.Id,
                HistoriaClinicaId = d.HistoriaClinicaId,
                PiezaDentalId = d.PiezaDentalId,
                PiezaCodigo = d.PiezaDental.Codigo,
                CaraDentalId = d.CaraDentalId,
                CaraDentalNombre = d.CaraDental != null ? d.CaraDental.Nombre : null,
                EstadoId = d.EstadoId,
                EstadoNombre = d.Estado.Nombre,
                Observacion = d.Observacion,
                FechaRegistro = d.FechaRegistro
            }).ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<OdontogramaDetalleDto>> Get(int id)
    {
        var item = await _context.OdontogramaDetalles
            .AsNoTracking()
            .Where(d => d.Id == id)
            .Select(d => new OdontogramaDetalleDto
            {
                Id = d.Id,
                HistoriaClinicaId = d.HistoriaClinicaId,
                PiezaDentalId = d.PiezaDentalId,
                PiezaCodigo = d.PiezaDental.Codigo,
                CaraDentalId = d.CaraDentalId,
                CaraDentalNombre = d.CaraDental != null ? d.CaraDental.Nombre : null,
                EstadoId = d.EstadoId,
                EstadoNombre = d.Estado.Nombre,
                Observacion = d.Observacion,
                FechaRegistro = d.FechaRegistro
            }).FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Detalle odontograma no encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<OdontogramaDetalleDto>> Crear([FromBody] CrearOdontogramaDetalleDto dto)
    {
        var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId);
        if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });

        var piezaExiste = await _context.PiezasDentales.AnyAsync(p => p.Id == dto.PiezaDentalId);
        if (!piezaExiste) return BadRequest(new { mensaje = "Pieza dental no encontrada." });

        if (dto.CaraDentalId.HasValue)
        {
            var caraExiste = await _context.CarasDentales.AnyAsync(c => c.Id == dto.CaraDentalId.Value);
            if (!caraExiste) return BadRequest(new { mensaje = "Cara dental no encontrada." });
        }

        var estadoExiste = await _context.EstadosPiezaOdontogramas.AnyAsync(e => e.Id == dto.EstadoId);
        if (!estadoExiste) return BadRequest(new { mensaje = "Estado de pieza no encontrado." });

        var ent = new Data.Models.OdontogramaDetalle
        {
            HistoriaClinicaId = dto.HistoriaClinicaId,
            PiezaDentalId = dto.PiezaDentalId,
            CaraDentalId = dto.CaraDentalId,
            EstadoId = dto.EstadoId,
            Observacion = dto.Observacion,
            FechaRegistro = DateTime.UtcNow
        };

        _context.OdontogramaDetalles.Add(ent);
        await _context.SaveChangesAsync();

        var created = await _context.OdontogramaDetalles
            .AsNoTracking()
            .Where(d => d.Id == ent.Id)
            .Select(d => new OdontogramaDetalleDto
            {
                Id = d.Id,
                HistoriaClinicaId = d.HistoriaClinicaId,
                PiezaDentalId = d.PiezaDentalId,
                PiezaCodigo = d.PiezaDental.Codigo,
                CaraDentalId = d.CaraDentalId,
                CaraDentalNombre = d.CaraDental != null ? d.CaraDental.Nombre : null,
                EstadoId = d.EstadoId,
                EstadoNombre = d.Estado.Nombre,
                Observacion = d.Observacion,
                FechaRegistro = d.FechaRegistro
            }).FirstAsync();

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarOdontogramaDetalleDto dto)
    {
        var ent = await _context.OdontogramaDetalles.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Detalle odontograma no encontrado." });

        if (dto.PiezaDentalId.HasValue)
        {
            var piezaExiste = await _context.PiezasDentales.AnyAsync(p => p.Id == dto.PiezaDentalId.Value);
            if (!piezaExiste) return BadRequest(new { mensaje = "Pieza dental no encontrada." });
            ent.PiezaDentalId = dto.PiezaDentalId.Value;
        }
        if (dto.CaraDentalId.HasValue) ent.CaraDentalId = dto.CaraDentalId;
        if (dto.EstadoId.HasValue)
        {
            var estadoExiste = await _context.EstadosPiezaOdontogramas.AnyAsync(e => e.Id == dto.EstadoId.Value);
            if (!estadoExiste) return BadRequest(new { mensaje = "Estado de pieza no encontrado." });
            ent.EstadoId = dto.EstadoId.Value;
        }
        if (dto.Observacion != null) ent.Observacion = dto.Observacion;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _context.OdontogramaDetalles.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Detalle odontograma no encontrado." });
        _context.OdontogramaDetalles.Remove(ent);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
