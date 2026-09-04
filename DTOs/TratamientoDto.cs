using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class TratamientoDto
{
    public int Id { get; set; }
    public int HistoriaClinicaId { get; set; }
    public int TipoTratamientoId { get; set; }
    [StringLength(200)]
    public string TipoTratamientoNombre { get; set; } = string.Empty;
    public short? PiezaDentalId { get; set; }
    public short? PiezaCodigo { get; set; }
    public decimal Precio { get; set; }
    [StringLength(500)]
    public string? Observacion { get; set; }
    public DateOnly FechaAplicacion { get; set; }
    [StringLength(50)]
    public string Estado { get; set; } = string.Empty;
}
