using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class MovimientoCuentaActualizarDto
{
    public int? HistoriaClinicaId { get; set; }
    public int? TratamientoId { get; set; }

    [Required]
    public int TipoMovimientoId { get; set; }

    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    [MaxLength(200)]
    public string Concepto { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Debe { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Haber { get; set; }
}