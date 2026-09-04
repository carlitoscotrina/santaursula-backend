using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class HistoriaClinicaDto
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    [StringLength(200)]
    public string? PacienteNombre { get; set; }
    [StringLength(50)]
    public string? NumeroHistoria { get; set; }
    public DateTime FechaHoraAtencion { get; set; }
    public int ProfesionalId { get; set; }
    [StringLength(200)]
    public string? ProfesionalNombre { get; set; }
    [StringLength(300)]
    public string? MotivoConsulta { get; set; }
    public bool? Dolor { get; set; }
    [StringLength(200)]
    public string? TiempoEnfermedad { get; set; }
    [StringLength(500)]
    public string? AntecedentesPatologicos { get; set; }
    [StringLength(500)]
    public string? AntecedentesFamiliares { get; set; }
    [StringLength(300)]
    public string? MedicacionActual { get; set; }
    public bool? ExtraccionesPrevias { get; set; }
    public bool? ProblemasPostExtraccion { get; set; }
    public bool? HemorragiaExcesiva { get; set; }
    public DateOnly? UltimaVisitaOdontologo { get; set; }
    [StringLength(20)]
    public string? Gestante { get; set; }
    [StringLength(1000)]
    public string? Observaciones { get; set; }
    public byte EstadoId { get; set; }
    [StringLength(100)]
    public string? EstadoNombre { get; set; }
    public DateTime? FechaCierre { get; set; }
    public DateTime FechaCreacion { get; set; }
}
