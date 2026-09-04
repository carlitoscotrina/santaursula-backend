using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearMovimientoCuentaDto
{
    [Required]
    public int PacienteId { get; set; }

    public int? HistoriaClinicaId { get; set; }
    public int? TratamientoId { get; set; }

    [Required]
    public byte TipoMovimientoId { get; set; }

    [Required]
    public DateOnly Fecha { get; set; }

    [Required]
    [StringLength(250)]
    public string Concepto { get; set; } = string.Empty;

    [Range(0, 9999999)]
    public decimal Debe { get; set; }

    [Range(0, 9999999)]
    public decimal Haber { get; set; }

    [Required]
    public int RegistradoPorUsuarioId { get; set; }
}
