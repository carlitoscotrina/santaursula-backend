using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearOdontogramaDetalleDto
{
    [Required]
    public int HistoriaClinicaId { get; set; }

    [Required]
    public short PiezaDentalId { get; set; }

    public byte? CaraDentalId { get; set; }

    [Required]
    public byte EstadoId { get; set; }

    [StringLength(500)]
    public string? Observacion { get; set; }
}
