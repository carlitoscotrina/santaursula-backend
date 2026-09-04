using System;

namespace SantaUrsula.API.DTOs;

public class ActualizarHistoriaClinicaDto
{
    public string? NumeroHistoria { get; set; }
    public DateTime? FechaHoraAtencion { get; set; }
    public int? ProfesionalId { get; set; }
    public string? MotivoConsulta { get; set; }
    public bool? Dolor { get; set; }
    public string? TiempoEnfermedad { get; set; }
    public string? AntecedentesPatologicos { get; set; }
    public string? AntecedentesFamiliares { get; set; }
    public string? MedicacionActual { get; set; }
    public bool? ExtraccionesPrevias { get; set; }
    public bool? ProblemasPostExtraccion { get; set; }
    public bool? HemorragiaExcesiva { get; set; }
    public DateOnly? UltimaVisitaOdontologo { get; set; }
    public string? Gestante { get; set; }
    public string? Observaciones { get; set; }
    public byte? EstadoId { get; set; }
    public DateTime? FechaCierre { get; set; }
}
