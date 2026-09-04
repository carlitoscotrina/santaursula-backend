using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class AuditoriaDto
{
    public long Id { get; set; }
    public int? UsuarioId { get; set; }
    [StringLength(100)]
    public string? UsuarioNombre { get; set; }
    public DateTime FechaHora { get; set; }
    [StringLength(50)]
    public string Accion { get; set; } = string.Empty;
    [StringLength(100)]
    public string TablaAfectada { get; set; } = string.Empty;
    [StringLength(50)]
    public string RegistroAfectadoId { get; set; } = string.Empty;
    [StringLength(2000)]
    public string? ValorAnterior { get; set; }
    [StringLength(2000)]
    public string? ValorNuevo { get; set; }
    [StringLength(45)]
    public string? DireccionIP { get; set; }
}
