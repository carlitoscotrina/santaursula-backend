using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class ActualizarCitaDto
{
    public DateOnly? Fecha { get; set; }
    public TimeOnly? Hora { get; set; }
        [StringLength(500)]
        public string? Motivo { get; set; }
    public byte? EstadoId { get; set; }
    public int? HistoriaClinicaId { get; set; }
}
