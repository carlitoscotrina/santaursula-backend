using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearCitaDto
{
    [Required]
    public int PacienteId { get; set; }

    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    public TimeOnly Hora { get; set; }

    [StringLength(500)]
    public string? Motivo { get; set; }

    [Required]
    public byte EstadoId { get; set; }

    public int? HistoriaClinicaId { get; set; }

    [Required]
    public int CreadoPorUsuarioId { get; set; }
}
