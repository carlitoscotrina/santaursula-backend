using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class MovimientoCuentaDto
{
    public int Id { get; set; }
    public int PacienteId { get; set; }
    public int? HistoriaClinicaId { get; set; }
    public int? TratamientoId { get; set; }
    public byte TipoMovimientoId { get; set; }
    [StringLength(100)]
    public string TipoMovimientoNombre { get; set; } = string.Empty;
    public DateOnly Fecha { get; set; }
    [StringLength(250)]
    public string Concepto { get; set; } = string.Empty;
    public decimal Debe { get; set; }
    public decimal Haber { get; set; }
    public int RegistradoPorUsuarioId { get; set; }
    [StringLength(200)]
    public string RegistradoPorUsuarioNombre { get; set; } = string.Empty;
    public DateTime FechaRegistro { get; set; }
}
