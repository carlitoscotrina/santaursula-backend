using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientosCuentaController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public MovimientosCuentaController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovimientoCuentaDto>>> Get()
    {
        var items = await _context.MovimientosCuenta.AsNoTracking().Select(m => new MovimientoCuentaDto
        {
            Id = m.Id,
            PacienteId = m.PacienteId,
            HistoriaClinicaId = m.HistoriaClinicaId,
            TratamientoId = m.TratamientoId,
            TipoMovimientoId = m.TipoMovimientoId,
            TipoMovimientoNombre = m.TipoMovimiento.Nombre,
            Fecha = m.Fecha,
            Concepto = m.Concepto,
            Debe = m.Debe,
            Haber = m.Haber,
            RegistradoPorUsuarioId = m.RegistradoPorUsuarioId,
            RegistradoPorUsuarioNombre = m.RegistradoPorUsuario.NombreCompleto,
            FechaRegistro = m.FechaRegistro
        }).ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MovimientoCuentaDto>> Get(int id)
    {
        var item = await _context.MovimientosCuenta.AsNoTracking().Where(m => m.Id == id).Select(m => new MovimientoCuentaDto
        {
            Id = m.Id,
            PacienteId = m.PacienteId,
            HistoriaClinicaId = m.HistoriaClinicaId,
            TratamientoId = m.TratamientoId,
            TipoMovimientoId = m.TipoMovimientoId,
            TipoMovimientoNombre = m.TipoMovimiento.Nombre,
            Fecha = m.Fecha,
            Concepto = m.Concepto,
            Debe = m.Debe,
            Haber = m.Haber,
            RegistradoPorUsuarioId = m.RegistradoPorUsuarioId,
            RegistradoPorUsuarioNombre = m.RegistradoPorUsuario.NombreCompleto,
            FechaRegistro = m.FechaRegistro
        }).FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Movimiento no encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<MovimientoCuentaDto>> Crear([FromBody] CrearMovimientoCuentaDto dto)
    {
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == dto.PacienteId);
        if (!pacienteExiste) return BadRequest(new { mensaje = "Paciente no encontrado." });

        if (dto.HistoriaClinicaId.HasValue)
        {
            var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId.Value);
            if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });
        }

        if (dto.TratamientoId.HasValue)
        {
            var trExiste = await _context.Tratamientos.AnyAsync(t => t.Id == dto.TratamientoId.Value);
            if (!trExiste) return BadRequest(new { mensaje = "Tratamiento no encontrado." });
        }

        var tipoExists = await _context.TiposMovimientoCuenta.AnyAsync(t => t.Id == dto.TipoMovimientoId);
        if (!tipoExists) return BadRequest(new { mensaje = "Tipo de movimiento no encontrado." });

        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == dto.RegistradoPorUsuarioId);
        if (!usuarioExiste) return BadRequest(new { mensaje = "Usuario registrador no encontrado." });

        var ent = new Data.Models.MovimientosCuentum
        {
            PacienteId = dto.PacienteId,
            HistoriaClinicaId = dto.HistoriaClinicaId,
            TratamientoId = dto.TratamientoId,
            TipoMovimientoId = dto.TipoMovimientoId,
            Fecha = dto.Fecha,
            Concepto = dto.Concepto,
            Debe = dto.Debe,
            Haber = dto.Haber,
            RegistradoPorUsuarioId = dto.RegistradoPorUsuarioId,
            FechaRegistro = DateTime.Now
        };

        _context.MovimientosCuenta.Add(ent);
        await _context.SaveChangesAsync();

        var created = new MovimientoCuentaDto
        {
            Id = ent.Id,
            PacienteId = ent.PacienteId,
            HistoriaClinicaId = ent.HistoriaClinicaId,
            TratamientoId = ent.TratamientoId,
            TipoMovimientoId = ent.TipoMovimientoId,
            TipoMovimientoNombre = (await _context.TiposMovimientoCuenta.FindAsync(ent.TipoMovimientoId))?.Nombre ?? string.Empty,
            Fecha = ent.Fecha,
            Concepto = ent.Concepto,
            Debe = ent.Debe,
            Haber = ent.Haber,
            RegistradoPorUsuarioId = ent.RegistradoPorUsuarioId,
            RegistradoPorUsuarioNombre = (await _context.Usuarios.FindAsync(ent.RegistradoPorUsuarioId))?.NombreCompleto ?? string.Empty,
            FechaRegistro = ent.FechaRegistro
        };

        return CreatedAtAction(nameof(Get), new { id = ent.Id }, created);
    }

[HttpPut("{id:int}")]
public async Task<IActionResult> Actualizar(
    int id,
    [FromBody] CrearMovimientoCuentaDto dto)
{
    var movimiento = await _context.MovimientosCuenta.FindAsync(id);

    if (movimiento == null)
        return NotFound(new { mensaje = "Movimiento no encontrado." });

    if (dto.HistoriaClinicaId.HasValue)
    {
        var historiaExiste = await _context.HistoriasClinicas
            .AnyAsync(h => h.Id == dto.HistoriaClinicaId.Value);

        if (!historiaExiste)
            return BadRequest(new { mensaje = "Historia clínica no encontrada." });
    }

    if (dto.TratamientoId.HasValue)
    {
        var tratamientoExiste = await _context.Tratamientos
            .AnyAsync(t => t.Id == dto.TratamientoId.Value);

        if (!tratamientoExiste)
            return BadRequest(new { mensaje = "Tratamiento no encontrado." });
    }

    var tipoExiste = await _context.TiposMovimientoCuenta
        .AnyAsync(t => t.Id == dto.TipoMovimientoId);

    if (!tipoExiste)
        return BadRequest(new
        {
            mensaje = "El tipo de movimiento indicado no existe."
        });

    movimiento.HistoriaClinicaId = dto.HistoriaClinicaId;
    movimiento.TratamientoId = dto.TratamientoId;
    movimiento.TipoMovimientoId = dto.TipoMovimientoId;
    movimiento.Fecha = dto.Fecha;
    movimiento.Concepto = dto.Concepto;
    movimiento.Debe = dto.Debe;
    movimiento.Haber = dto.Haber;

    await _context.SaveChangesAsync();

    return NoContent();
}

[HttpDelete("{id:int}")]
public async Task<IActionResult> Eliminar(int id)
{
    var movimiento = await _context.MovimientosCuenta.FindAsync(id);

    if (movimiento == null)
        return NotFound(new { mensaje = "Movimiento no encontrado." });

    _context.MovimientosCuenta.Remove(movimiento);
    await _context.SaveChangesAsync();

    return NoContent();
}
}
