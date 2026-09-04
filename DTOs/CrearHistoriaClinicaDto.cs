using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearHistoriaClinicaDto
{
    [Required]
    public int PacienteId { get; set; }

    [StringLength(50)]
    public string? NumeroHistoria { get; set; }

    [Required]
    public DateTime FechaHoraAtencion { get; set; }

    [Required]
    public int ProfesionalId { get; set; }

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
    [Required]
    public byte EstadoId { get; set; }
}
