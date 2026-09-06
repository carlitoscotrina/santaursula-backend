using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;
using SantaUrsula.API.Data.Models;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitasController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;

    public CitasController(SantaUrsulaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CitaDto>>> Get()
    {
        var items = await _context.Citas
            .AsNoTracking()
            .Select(c => new CitaDto
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                PacienteNombre = c.Paciente.Nombres,
                Fecha = c.Fecha,
                Hora = c.Hora,
                Motivo = c.Motivo,
                EstadoId = c.EstadoId,
                EstadoNombre = c.Estado.Nombre,
                HistoriaClinicaId = c.HistoriaClinicaId,
                CreadoPorUsuarioId = c.CreadoPorUsuarioId,
                FechaCreacion = c.FechaCreacion
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CitaDto>> Get(int id)
    {
        var item = await _context.Citas
            .AsNoTracking()
            .Where(c => c.Id == id)
            .Select(c => new CitaDto
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                PacienteNombre = c.Paciente.Nombres,
                Fecha = c.Fecha,
                Hora = c.Hora,
                Motivo = c.Motivo,
                EstadoId = c.EstadoId,
                EstadoNombre = c.Estado.Nombre,
                HistoriaClinicaId = c.HistoriaClinicaId,
                CreadoPorUsuarioId = c.CreadoPorUsuarioId,
                FechaCreacion = c.FechaCreacion
            })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Cita no encontrada." });

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<CitaDto>> Crear([FromBody] CrearCitaDto dto)
    {
        // Validaciones básicas
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == dto.PacienteId);
        if (!pacienteExiste) return BadRequest(new { mensaje = "Paciente no encontrado." });

        var estadoExiste = await _context.EstadosCita.AnyAsync(e => e.Id == dto.EstadoId);
        if (!estadoExiste) return BadRequest(new { mensaje = "Estado no encontrado." });

        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == dto.CreadoPorUsuarioId);
        if (!usuarioExiste) return BadRequest(new { mensaje = "Usuario creador no encontrado." });

        var cita = new Cita
        {
            PacienteId = dto.PacienteId,
            Fecha = dto.Fecha,
            Hora = dto.Hora,
            Motivo = dto.Motivo?.Trim(),
            EstadoId = dto.EstadoId,
            HistoriaClinicaId = dto.HistoriaClinicaId,
            CreadoPorUsuarioId = dto.CreadoPorUsuarioId,
            FechaCreacion = DateTime.UtcNow
        };

        _context.Citas.Add(cita);
        await _context.SaveChangesAsync();

        var creado = await _context.Citas
            .AsNoTracking()
            .Where(c => c.Id == cita.Id)
            .Select(c => new CitaDto
            {
                Id = c.Id,
                PacienteId = c.PacienteId,
                PacienteNombre = c.Paciente.Nombres,
                Fecha = c.Fecha,
                Hora = c.Hora,
                Motivo = c.Motivo,
                EstadoId = c.EstadoId,
                EstadoNombre = c.Estado.Nombre,
                HistoriaClinicaId = c.HistoriaClinicaId,
                CreadoPorUsuarioId = c.CreadoPorUsuarioId,
                FechaCreacion = c.FechaCreacion
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(Get), new { id = creado!.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarCitaDto dto)
    {
        var cita = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id);
        if (cita == null) return NotFound(new { mensaje = "Cita no encontrada." });

        if (dto.Fecha.HasValue) cita.Fecha = dto.Fecha.Value;
        if (dto.Hora.HasValue) cita.Hora = dto.Hora.Value;
        if (dto.Motivo != null) cita.Motivo = dto.Motivo.Trim();
        if (dto.EstadoId.HasValue)
        {
            var estadoExiste = await _context.EstadosCita.AnyAsync(e => e.Id == dto.EstadoId.Value);
            if (!estadoExiste) return BadRequest(new { mensaje = "Estado no encontrado." });
            cita.EstadoId = dto.EstadoId.Value;
        }

        if (dto.HistoriaClinicaId.HasValue)
        {
            var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId.Value);
            if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });
            cita.HistoriaClinicaId = dto.HistoriaClinicaId.Value;
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var cita = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id);
        if (cita == null) return NotFound(new { mensaje = "Cita no encontrada." });

        _context.Citas.Remove(cita);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
