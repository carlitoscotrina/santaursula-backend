using System;

namespace SantaUrsula.API.DTOs;

public class PacienteDto
{
    public int Id { get; set; }
    public string? DNI { get; set; }
    public string? Nombres { get; set; }
    public string? ApellidoPaterno { get; set; }
    public string? ApellidoMaterno { get; set; }
    public DateOnly FechaNacimiento { get; set; }
    public byte SexoId { get; set; }
    public string? Sexo { get; set; }
    public string? Ocupacion { get; set; }
    public string? Religion { get; set; }
    public string? EstadoCivil { get; set; }
    public string? LugarNacimiento { get; set; }
    public string? LugarProcedencia { get; set; }
    public string? DomicilioActual { get; set; }
    public string? Celular { get; set; }
    public string? Email { get; set; }
    public string? Alergias { get; set; }
    public string? NombreAcompanante { get; set; }
    public string? CelularAcompanante { get; set; }
    public DateTime FechaRegistro { get; set; }
    public bool Activo { get; set; }
}
