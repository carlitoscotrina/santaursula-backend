using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearAuditoriaDto
{
    public int? UsuarioId { get; set; }

    [Required]
    [StringLength(20)]
    public string Accion { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string TablaAfectada { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string RegistroAfectadoId { get; set; } = string.Empty;

    public string? ValorAnterior { get; set; }
    public string? ValorNuevo { get; set; }
    [StringLength(45)]
    public string? DireccionIP { get; set; }
}
