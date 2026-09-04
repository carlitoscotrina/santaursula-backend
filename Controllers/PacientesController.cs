using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.Data.Models;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PacientesController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;

    public PacientesController(SantaUrsulaDbContext context)
    {
        _context = context;
    }

    // =========================================================
    // GET: api/Pacientes
    // =========================================================
    [HttpGet]
    public async Task<ActionResult<IEnumerable<PacienteDto>>> GetPacientes()
    {
        var pacientes = await _context.Pacientes
            .AsNoTracking()
            .Select(p => new PacienteDto
            {
                Id = p.Id,
                DNI = p.DNI,
                Nombres = p.Nombres,
                ApellidoPaterno = p.ApellidoPaterno,
                ApellidoMaterno = p.ApellidoMaterno,
                FechaNacimiento = p.FechaNacimiento,
                SexoId = p.SexoId,
                Sexo = p.Sexo.Nombre,
                Ocupacion = p.Ocupacion,
                Religion = p.Religion,
                EstadoCivil = p.EstadoCivil,
                LugarNacimiento = p.LugarNacimiento,
                LugarProcedencia = p.LugarProcedencia,
                DomicilioActual = p.DomicilioActual,
                Celular = p.Celular,
                Email = p.Email,
                Alergias = p.Alergias,
                NombreAcompanante = p.NombreAcompanante,
                CelularAcompanante = p.CelularAcompanante,
                FechaRegistro = p.FechaRegistro,
                Activo = p.Activo
            })
            .ToListAsync();

        return Ok(pacientes);
    }

    // =========================================================
    // GET: api/Pacientes/5
    // =========================================================
    [HttpGet("{id:int}")]
    public async Task<ActionResult<PacienteDto>> GetPaciente(int id)
    {
        var paciente = await _context.Pacientes
            .AsNoTracking()
            .Where(p => p.Id == id)
            .Select(p => new PacienteDto
            {
                Id = p.Id,
                DNI = p.DNI,
                Nombres = p.Nombres,
                ApellidoPaterno = p.ApellidoPaterno,
                ApellidoMaterno = p.ApellidoMaterno,
                FechaNacimiento = p.FechaNacimiento,
                SexoId = p.SexoId,
                Sexo = p.Sexo.Nombre,
                Ocupacion = p.Ocupacion,
                Religion = p.Religion,
                EstadoCivil = p.EstadoCivil,
                LugarNacimiento = p.LugarNacimiento,
                LugarProcedencia = p.LugarProcedencia,
                DomicilioActual = p.DomicilioActual,
                Celular = p.Celular,
                Email = p.Email,
                Alergias = p.Alergias,
                NombreAcompanante = p.NombreAcompanante,
                CelularAcompanante = p.CelularAcompanante,
                FechaRegistro = p.FechaRegistro,
                Activo = p.Activo
            })
            .FirstOrDefaultAsync();

        if (paciente == null)
        {
            return NotFound(new
            {
                mensaje = "Paciente no encontrado."
            });
        }

        return Ok(paciente);
    }

    // =========================================================
    // POST: api/Pacientes
    // =========================================================
    [HttpPost]
    public async Task<ActionResult<PacienteDto>> CrearPaciente(
        [FromBody] PacienteCrearDto dto)
    {
        // -----------------------------------------------------
        // Validar DNI
        // -----------------------------------------------------
        if (string.IsNullOrWhiteSpace(dto.DNI))
        {
            return BadRequest(new
            {
                mensaje = "El DNI es obligatorio."
            });
        }

        var dni = dto.DNI.Trim();

        var dniExiste = await _context.Pacientes
            .AnyAsync(p => p.DNI == dni);

        if (dniExiste)
        {
            return Conflict(new
            {
                mensaje = "Ya existe un paciente registrado con ese DNI."
            });
        }

        // -----------------------------------------------------
        // Validar sexo
        // -----------------------------------------------------
        var sexoExiste = await _context.Sexos
            .AnyAsync(s => s.Id == dto.SexoId);

        if (!sexoExiste)
        {
            return BadRequest(new
            {
                mensaje = $"No existe un sexo con el ID {dto.SexoId}."
            });
        }

        // -----------------------------------------------------
        // Crear entidad
        // -----------------------------------------------------
        var paciente = new Paciente
        {
            Id = 0,

            DNI = dni,
            Nombres = dto.Nombres?.Trim() ?? string.Empty,
            ApellidoPaterno = dto.ApellidoPaterno?.Trim() ?? string.Empty,
            ApellidoMaterno = dto.ApellidoMaterno?.Trim(),

            FechaNacimiento = dto.FechaNacimiento,

            SexoId = dto.SexoId,

            Ocupacion = dto.Ocupacion?.Trim(),
            Religion = dto.Religion?.Trim(),
            EstadoCivil = dto.EstadoCivil?.Trim(),
            LugarNacimiento = dto.LugarNacimiento?.Trim(),
            LugarProcedencia = dto.LugarProcedencia?.Trim(),
            DomicilioActual = dto.DomicilioActual?.Trim(),
            Celular = dto.Celular?.Trim(),
            Email = dto.Email?.Trim(),
            Alergias = dto.Alergias?.Trim(),

            NombreAcompanante = dto.NombreAcompanante?.Trim(),
            CelularAcompanante = dto.CelularAcompanante?.Trim(),

            FechaRegistro = DateTime.Now,
            Activo = true
        };

        // -----------------------------------------------------
        // Guardar
        // -----------------------------------------------------
        _context.Pacientes.Add(paciente);

        await _context.SaveChangesAsync();

        // -----------------------------------------------------
        // Preparar respuesta
        // -----------------------------------------------------
        var respuesta = new PacienteDto
        {
            Id = paciente.Id,
            DNI = paciente.DNI,
            Nombres = paciente.Nombres,
            ApellidoPaterno = paciente.ApellidoPaterno,
            ApellidoMaterno = paciente.ApellidoMaterno,
            FechaNacimiento = paciente.FechaNacimiento,
            SexoId = paciente.SexoId,
            Ocupacion = paciente.Ocupacion,
            Religion = paciente.Religion,
            EstadoCivil = paciente.EstadoCivil,
            LugarNacimiento = paciente.LugarNacimiento,
            LugarProcedencia = paciente.LugarProcedencia,
            DomicilioActual = paciente.DomicilioActual,
            Celular = paciente.Celular,
            Email = paciente.Email,
            Alergias = paciente.Alergias,
            NombreAcompanante = paciente.NombreAcompanante,
            CelularAcompanante = paciente.CelularAcompanante,
            FechaRegistro = paciente.FechaRegistro,
            Activo = paciente.Activo
        };

        // Añadir nombre del sexo a la respuesta
        var sexo = await _context.Sexos.FindAsync(paciente.SexoId);
        respuesta.Sexo = sexo?.Nombre;

        return CreatedAtAction(
            nameof(GetPaciente),
            new { id = paciente.Id },
            respuesta
        );

    }

    // =========================================================
    // PUT: api/Pacientes/5
    // =========================================================
    [HttpPut("{id:int}")]
    public async Task<IActionResult> ActualizarPaciente(
        int id,
        [FromBody] PacienteActualizarDto dto)
    {
        var paciente = await _context.Pacientes
            .FirstOrDefaultAsync(p => p.Id == id);

        if (paciente == null)
        {
            return NotFound(new { mensaje = "Paciente no encontrado." });
        }

        // -----------------------------------------------------
        // Validar DNI duplicado
        // -----------------------------------------------------
        if (!string.IsNullOrWhiteSpace(dto.DNI))
        {
            var dni = dto.DNI.Trim();

            var dniExiste = await _context.Pacientes
                .AnyAsync(p => p.DNI == dni && p.Id != id);

            if (dniExiste)
            {
                return Conflict(new { mensaje = "Otro paciente ya tiene registrado ese DNI." });
            }

            paciente.DNI = dni;
        }

        // -----------------------------------------------------
        // Validar sexo
        // -----------------------------------------------------
        if (dto.SexoId.HasValue)
        {
            var sexoExiste = await _context.Sexos
                .AnyAsync(s => s.Id == dto.SexoId.Value);

            if (!sexoExiste)
            {
                return BadRequest(new { mensaje = $"No existe un sexo con el ID {dto.SexoId.Value}." });
            }

            paciente.SexoId = (byte)dto.SexoId.Value;
        }

        // -----------------------------------------------------
        // Actualizar datos
        // -----------------------------------------------------
        if (dto.Nombres != null)
            paciente.Nombres = dto.Nombres.Trim();

        if (dto.ApellidoPaterno != null)
            paciente.ApellidoPaterno = dto.ApellidoPaterno.Trim();

        if (dto.ApellidoMaterno != null)
            paciente.ApellidoMaterno = dto.ApellidoMaterno.Trim();

        if (dto.FechaNacimiento.HasValue)
            paciente.FechaNacimiento = dto.FechaNacimiento.Value;

        if (dto.Ocupacion != null)
            paciente.Ocupacion = dto.Ocupacion.Trim();

        if (dto.Religion != null)
            paciente.Religion = dto.Religion.Trim();

        if (dto.EstadoCivil != null)
            paciente.EstadoCivil = dto.EstadoCivil.Trim();

        if (dto.LugarNacimiento != null)
            paciente.LugarNacimiento = dto.LugarNacimiento.Trim();

        if (dto.LugarProcedencia != null)
            paciente.LugarProcedencia = dto.LugarProcedencia.Trim();

        if (dto.DomicilioActual != null)
            paciente.DomicilioActual = dto.DomicilioActual.Trim();

        if (dto.Celular != null)
            paciente.Celular = dto.Celular.Trim();

        if (dto.Email != null)
            paciente.Email = dto.Email.Trim();

        if (dto.Alergias != null)
            paciente.Alergias = dto.Alergias.Trim();

        if (dto.NombreAcompanante != null)
            paciente.NombreAcompanante = dto.NombreAcompanante.Trim();

        if (dto.CelularAcompanante != null)
            paciente.CelularAcompanante = dto.CelularAcompanante.Trim();

        if (dto.Activo.HasValue)
            paciente.Activo = dto.Activo.Value;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // =========================================================
    // DELETE: api/Pacientes/5
    // =========================================================
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> EliminarPaciente(int id)
    {
        var paciente = await _context.Pacientes
            .FirstOrDefaultAsync(p => p.Id == id);

        if (paciente == null)
        {
            return NotFound(new
            {
                mensaje = "Paciente no encontrado."
            });
        }

        // Eliminación lógica
        paciente.Activo = false;

        await _context.SaveChangesAsync();

        return NoContent();
    }
}
