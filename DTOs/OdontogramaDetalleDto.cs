using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class OdontogramaDetalleDto
{
    public int Id { get; set; }
    public int HistoriaClinicaId { get; set; }
    public short PiezaDentalId { get; set; }
    public short PiezaCodigo { get; set; }
    public byte? CaraDentalId { get; set; }
    [StringLength(100)]
    public string? CaraDentalNombre { get; set; }
    public byte EstadoId { get; set; }
    [StringLength(100)]
    public string EstadoNombre { get; set; } = string.Empty;
    [StringLength(500)]
    public string? Observacion { get; set; }
    public DateTime FechaRegistro { get; set; }
}
