using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class ActualizarOdontogramaDetalleDto
{
    public short? PiezaDentalId { get; set; }
    public byte? CaraDentalId { get; set; }
    public byte? EstadoId { get; set; }
    [StringLength(500)]
    public string? Observacion { get; set; }
}
