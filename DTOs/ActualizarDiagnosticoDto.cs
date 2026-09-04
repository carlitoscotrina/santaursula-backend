using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class ActualizarDiagnosticoDto
{
    [StringLength(50)]
    public string? CodigoCIE10 { get; set; }

    [StringLength(200)]
    public string? Nombre { get; set; }

    [StringLength(1000)]
    public string? Descripcion { get; set; }

    public bool? Activo { get; set; }
}
