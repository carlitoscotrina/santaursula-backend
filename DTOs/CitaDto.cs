using System;

namespace SantaUrsula.API.DTOs;

public class CitaDto
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public string? PacienteNombre { get; set; }
    public DateOnly Fecha { get; set; }
    public TimeOnly Hora { get; set; }
    public string? Motivo { get; set; }
    public byte EstadoId { get; set; }
    public string? EstadoNombre { get; set; }
    public int? HistoriaClinicaId { get; set; }
    public int CreadoPorUsuarioId { get; set; }
    public DateTime FechaCreacion { get; set; }
}
