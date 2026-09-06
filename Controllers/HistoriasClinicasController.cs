using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HistoriasClinicasController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public HistoriasClinicasController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistoriaClinicaDto>>> Get()
    {
        var items = await _context.HistoriasClinicas
            .AsNoTracking()
            .Select(h => new HistoriaClinicaDto
            {
                Id = h.Id,
                PacienteId = h.PacienteId,
                PacienteNombre = h.Paciente.Nombres,
                NumeroHistoria = h.NumeroHistoria,
                FechaHoraAtencion = h.FechaHoraAtencion,
                ProfesionalId = h.ProfesionalId,
                ProfesionalNombre = h.Profesional.NombreCompleto,
                MotivoConsulta = h.MotivoConsulta,
                Dolor = h.Dolor,
                TiempoEnfermedad = h.TiempoEnfermedad,
                AntecedentesPatologicos = h.AntecedentesPatologicos,
                AntecedentesFamiliares = h.AntecedentesFamiliares,
                MedicacionActual = h.MedicacionActual,
                ExtraccionesPrevias = h.ExtraccionesPrevias,
                ProblemasPostExtraccion = h.ProblemasPostExtraccion,
                HemorragiaExcesiva = h.HemorragiaExcesiva,
                UltimaVisitaOdontologo = h.UltimaVisitaOdontologo,
                Gestante = h.Gestante,
                Observaciones = h.Observaciones,
                EstadoId = h.EstadoId,
                EstadoNombre = h.Estado.Nombre,
                FechaCierre = h.FechaCierre,
                FechaCreacion = h.FechaCreacion
            })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<HistoriaClinicaDto>> Get(int id)
    {
        var item = await _context.HistoriasClinicas
            .AsNoTracking()
            .Where(h => h.Id == id)
            .Select(h => new HistoriaClinicaDto
            {
                Id = h.Id,
                PacienteId = h.PacienteId,
                PacienteNombre = h.Paciente.Nombres,
                NumeroHistoria = h.NumeroHistoria,
                FechaHoraAtencion = h.FechaHoraAtencion,
                ProfesionalId = h.ProfesionalId,
                ProfesionalNombre = h.Profesional.NombreCompleto,
                MotivoConsulta = h.MotivoConsulta,
                Dolor = h.Dolor,
                TiempoEnfermedad = h.TiempoEnfermedad,
                AntecedentesPatologicos = h.AntecedentesPatologicos,
                AntecedentesFamiliares = h.AntecedentesFamiliares,
                MedicacionActual = h.MedicacionActual,
                ExtraccionesPrevias = h.ExtraccionesPrevias,
                ProblemasPostExtraccion = h.ProblemasPostExtraccion,
                HemorragiaExcesiva = h.HemorragiaExcesiva,
                UltimaVisitaOdontologo = h.UltimaVisitaOdontologo,
                Gestante = h.Gestante,
                Observaciones = h.Observaciones,
                EstadoId = h.EstadoId,
                EstadoNombre = h.Estado.Nombre,
                FechaCierre = h.FechaCierre,
                FechaCreacion = h.FechaCreacion
            })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Historia clínica no encontrada." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<HistoriaClinicaDto>> Crear([FromBody] CrearHistoriaClinicaDto dto)
    {
        var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == dto.PacienteId);
        if (!pacienteExiste) return BadRequest(new { mensaje = "Paciente no encontrado." });

        var profesionalExiste = await _context.Usuarios.AnyAsync(u => u.Id == dto.ProfesionalId);
        if (!profesionalExiste) return BadRequest(new { mensaje = "Profesional no encontrado." });

        var estadoExiste = await _context.EstadosHistoria.AnyAsync(e => e.Id == dto.EstadoId);
        if (!estadoExiste) return BadRequest(new { mensaje = "Estado no encontrado." });

        var ent = new Data.Models.HistoriasClinica
        {
            PacienteId = dto.PacienteId,
            NumeroHistoria = dto.NumeroHistoria?.Trim(),
            FechaHoraAtencion = dto.FechaHoraAtencion,
            ProfesionalId = dto.ProfesionalId,
            MotivoConsulta = dto.MotivoConsulta?.Trim(),
            Dolor = dto.Dolor,
            TiempoEnfermedad = dto.TiempoEnfermedad?.Trim(),
            AntecedentesPatologicos = dto.AntecedentesPatologicos?.Trim(),
            AntecedentesFamiliares = dto.AntecedentesFamiliares?.Trim(),
            MedicacionActual = dto.MedicacionActual?.Trim(),
            ExtraccionesPrevias = dto.ExtraccionesPrevias,
            ProblemasPostExtraccion = dto.ProblemasPostExtraccion,
            HemorragiaExcesiva = dto.HemorragiaExcesiva,
            UltimaVisitaOdontologo = dto.UltimaVisitaOdontologo,
            Gestante = dto.Gestante?.Trim(),
            Observaciones = dto.Observaciones?.Trim(),
            EstadoId = dto.EstadoId,
            FechaCreacion = DateTime.UtcNow
        };

        _context.HistoriasClinicas.Add(ent);
        await _context.SaveChangesAsync();

        var creado = await _context.HistoriasClinicas
            .AsNoTracking()
            .Where(h => h.Id == ent.Id)
            .Select(h => new HistoriaClinicaDto
            {
                Id = h.Id,
                PacienteId = h.PacienteId,
                PacienteNombre = h.Paciente.Nombres,
                NumeroHistoria = h.NumeroHistoria,
                FechaHoraAtencion = h.FechaHoraAtencion,
                ProfesionalId = h.ProfesionalId,
                ProfesionalNombre = h.Profesional.NombreCompleto,
                MotivoConsulta = h.MotivoConsulta,
                Dolor = h.Dolor,
                TiempoEnfermedad = h.TiempoEnfermedad,
                AntecedentesPatologicos = h.AntecedentesPatologicos,
                AntecedentesFamiliares = h.AntecedentesFamiliares,
                MedicacionActual = h.MedicacionActual,
                ExtraccionesPrevias = h.ExtraccionesPrevias,
                ProblemasPostExtraccion = h.ProblemasPostExtraccion,
                HemorragiaExcesiva = h.HemorragiaExcesiva,
                UltimaVisitaOdontologo = h.UltimaVisitaOdontologo,
                Gestante = h.Gestante,
                Observaciones = h.Observaciones,
                EstadoId = h.EstadoId,
                EstadoNombre = h.Estado.Nombre,
                FechaCierre = h.FechaCierre,
                FechaCreacion = h.FechaCreacion
            })
            .FirstOrDefaultAsync();

        return CreatedAtAction(nameof(Get), new { id = creado!.Id }, creado);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Actualizar(int id, [FromBody] ActualizarHistoriaClinicaDto dto)
    {
        var ent = await _context.HistoriasClinicas.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Historia clinica no encontrada." });

        if (dto.NumeroHistoria != null) ent.NumeroHistoria = dto.NumeroHistoria.Trim();
        if (dto.FechaHoraAtencion.HasValue) ent.FechaHoraAtencion = dto.FechaHoraAtencion.Value;
        if (dto.ProfesionalId.HasValue)
        {
            var profExiste = await _context.Usuarios.AnyAsync(u => u.Id == dto.ProfesionalId.Value);
            if (!profExiste) return BadRequest(new { mensaje = "Profesional no encontrado." });
            ent.ProfesionalId = dto.ProfesionalId.Value;
        }

        if (dto.MotivoConsulta != null) ent.MotivoConsulta = dto.MotivoConsulta.Trim();
        if (dto.Dolor.HasValue) ent.Dolor = dto.Dolor.Value;
        if (dto.TiempoEnfermedad != null) ent.TiempoEnfermedad = dto.TiempoEnfermedad.Trim();
        if (dto.AntecedentesPatologicos != null) ent.AntecedentesPatologicos = dto.AntecedentesPatologicos.Trim();
        if (dto.AntecedentesFamiliares != null) ent.AntecedentesFamiliares = dto.AntecedentesFamiliares.Trim();
        if (dto.MedicacionActual != null) ent.MedicacionActual = dto.MedicacionActual.Trim();
        if (dto.ExtraccionesPrevias.HasValue) ent.ExtraccionesPrevias = dto.ExtraccionesPrevias.Value;
        if (dto.ProblemasPostExtraccion.HasValue) ent.ProblemasPostExtraccion = dto.ProblemasPostExtraccion.Value;
        if (dto.HemorragiaExcesiva.HasValue) ent.HemorragiaExcesiva = dto.HemorragiaExcesiva.Value;
        if (dto.UltimaVisitaOdontologo.HasValue) ent.UltimaVisitaOdontologo = dto.UltimaVisitaOdontologo.Value;
        if (dto.Gestante != null) ent.Gestante = dto.Gestante.Trim();
        if (dto.Observaciones != null) ent.Observaciones = dto.Observaciones.Trim();
        if (dto.EstadoId.HasValue) ent.EstadoId = dto.EstadoId.Value;
        if (dto.FechaCierre.HasValue) ent.FechaCierre = dto.FechaCierre;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var ent = await _context.HistoriasClinicas.FindAsync(id);
        if (ent == null) return NotFound(new { mensaje = "Historia clinica no encontrada." });

        var tieneDependientes =
            await _context.Citas.AnyAsync(c => c.HistoriaClinicaId == id) ||
            await _context.Tratamientos.AnyAsync(t => t.HistoriaClinicaId == id) ||
            await _context.Documentos.AnyAsync(d => d.HistoriaClinicaId == id) ||
            await _context.HistoriaDiagnosticos.AnyAsync(h => h.HistoriaClinicaId == id) ||
            await _context.OdontogramaDetalles.AnyAsync(o => o.HistoriaClinicaId == id) ||
            await _context.MovimientosCuenta.AnyAsync(m => m.HistoriaClinicaId == id) ||
            await _context.EvaluacionesPeriodontales.AnyAsync(e => e.HistoriaClinicaId == id);

        if (tieneDependientes)
        {
            return Conflict(new { mensaje = "No se puede eliminar: la historia clínica tiene citas, tratamientos, documentos u otros registros asociados." });
        }

        _context.HistoriasClinicas.Remove(ent);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
