using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearDiagnosticoDto
{
    [Required]
    [StringLength(50)]
    public string CodigoCIE10 { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Nombre { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Descripcion { get; set; }

    public bool Activo { get; set; } = true;
}
