using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class PacienteActualizarDto
{
    [StringLength(12)]
    public string? DNI { get; set; }
    [StringLength(100)]
    public string? Nombres { get; set; }
    [StringLength(100)]
    public string? ApellidoPaterno { get; set; }
    [StringLength(100)]
    public string? ApellidoMaterno { get; set; }
    public DateOnly? FechaNacimiento { get; set; }
    public byte? SexoId { get; set; }
    [StringLength(100)]
    public string? Ocupacion { get; set; }
    [StringLength(100)]
    public string? Religion { get; set; }
    [StringLength(50)]
    public string? EstadoCivil { get; set; }
    [StringLength(100)]
    public string? LugarNacimiento { get; set; }
    [StringLength(100)]
    public string? LugarProcedencia { get; set; }
    [StringLength(250)]
    public string? DomicilioActual { get; set; }
    [Phone]
    [StringLength(20)]
    public string? Celular { get; set; }
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }
    [StringLength(500)]
    public string? Alergias { get; set; }
    [StringLength(200)]
    public string? NombreAcompanante { get; set; }
    [Phone]
    [StringLength(20)]
    public string? CelularAcompanante { get; set; }
    public bool? Activo { get; set; }
}
