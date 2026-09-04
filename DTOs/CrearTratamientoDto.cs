using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearTratamientoDto
{
    [Required]
    public int HistoriaClinicaId { get; set; }

    [Required]
    public int TipoTratamientoId { get; set; }

    public short? PiezaDentalId { get; set; }

    [Range(0, 999999)]
    public decimal Precio { get; set; }

    [StringLength(500)]
    public string? Observacion { get; set; }

    [Required]
    public DateOnly FechaAplicacion { get; set; }

    [Required]
    [StringLength(50)]
    public string Estado { get; set; } = string.Empty;
}
