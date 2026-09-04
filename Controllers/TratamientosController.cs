using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TratamientosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public TratamientosController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TratamientoDto>>> Get()
    {
        var items = await _context.Tratamientos
            .AsNoTracking()
            .Select(t => new TratamientoDto
            {
                Id = t.Id,
                HistoriaClinicaId = t.HistoriaClinicaId,
                TipoTratamientoId = t.TipoTratamientoId,
                TipoTratamientoNombre = t.TipoTratamiento.Nombre,
                PiezaDentalId = t.PiezaDentalId,
                PiezaCodigo = t.PiezaDental != null ? t.PiezaDental.Codigo : (short?)null,
                Precio = t.Precio,
                Observacion = t.Observacion,
                FechaAplicacion = t.FechaAplicacion,
                Estado = t.Estado
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TratamientoDto>> Get(int id)
    {
        var item = await _context.Tratamientos
            .AsNoTracking()
            .Where(t => t.Id == id)
            .Select(t => new TratamientoDto
            {
                Id = t.Id,
                HistoriaClinicaId = t.HistoriaClinicaId,
                TipoTratamientoId = t.TipoTratamientoId,
                TipoTratamientoNombre = t.TipoTratamiento.Nombre,
                PiezaDentalId = t.PiezaDentalId,
                PiezaCodigo = t.PiezaDental != null ? t.PiezaDental.Codigo : (short?)null,
                Precio = t.Precio,
                Observacion = t.Observacion,
                FechaAplicacion = t.FechaAplicacion,
                Estado = t.Estado
            })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Tratamiento no encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<TratamientoDto>> Crear([FromBody] CrearTratamientoDto dto)
    {
        var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId);
        if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });

        var tipoExiste = await _context.TiposTratamientos.AnyAsync(tt => tt.Id == dto.TipoTratamientoId);
        if (!tipoExiste) return BadRequest(new { mensaje = "Tipo de tratamiento no encontrado." });

        if (dto.PiezaDentalId.HasValue)
        {
            var piezaExiste = await _context.PiezasDentales.AnyAsync(p => p.Id == dto.PiezaDentalId.Value);
            if (!piezaExiste) return BadRequest(new { mensaje = "Pieza dental no encontrada." });
        }

        var ent = new Data.Models.Tratamiento
        {
            HistoriaClinicaId = dto.HistoriaClinicaId,
            TipoTratamientoId = dto.TipoTratamientoId,
            PiezaDentalId = dto.PiezaDentalId,
            Precio = dto.Precio,
            Observacion = dto.Observacion,
            FechaAplicacion = dto.FechaAplicacion,
            Estado = dto.Estado
        };

        _context.Tratamientos.Add(ent);
        await _context.SaveChangesAsync();

        var created = await _context.Tratamientos.AsNoTracking().Where(t => t.Id == ent.Id).Select(t => new TratamientoDto
        {
            Id = t.Id,
            HistoriaClinicaId = t.HistoriaClinicaId,
            TipoTratamientoId = t.TipoTratamientoId,
            TipoTratamientoNombre = t.TipoTratamiento.Nombre,
            PiezaDentalId = t.PiezaDentalId,
            PiezaCodigo = t.PiezaDental != null ? t.PiezaDental.Codigo : (short?)null,
            Precio = t.Precio,
            Observacion = t.Observacion,
            FechaAplicacion = t.FechaAplicacion,
            Estado = t.Estado
        }).FirstAsync();

        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarTratamientoDto dto)
    {
        var ent = await _context.Tratamientos.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Tratamiento no encontrado." });

        if (dto.TipoTratamientoId.HasValue)
        {
            var tipoExiste = await _context.TiposTratamientos.AnyAsync(tt => tt.Id == dto.TipoTratamientoId.Value);
            if (!tipoExiste) return BadRequest(new { mensaje = "Tipo de tratamiento no encontrado." });
            ent.TipoTratamientoId = dto.TipoTratamientoId.Value;
        }
        if (dto.PiezaDentalId.HasValue) ent.PiezaDentalId = dto.PiezaDentalId;
        if (dto.Precio.HasValue) ent.Precio = dto.Precio.Value;
        if (dto.Observacion != null) ent.Observacion = dto.Observacion;
        if (dto.FechaAplicacion.HasValue) ent.FechaAplicacion = dto.FechaAplicacion.Value;
        if (dto.Estado != null) ent.Estado = dto.Estado;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _context.Tratamientos.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Tratamiento no encontrado." });

        var tieneMovimientos = await _context.MovimientosCuenta.AnyAsync(m => m.TratamientoId == id);
        if (tieneMovimientos)
        {
            return Conflict(new { mensaje = "No se puede eliminar: el tratamiento tiene movimientos de cuenta asociados." });
        }

        _context.Tratamientos.Remove(ent);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}