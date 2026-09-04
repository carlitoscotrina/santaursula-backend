using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class DiagnosticoDto
{
    public int Id { get; set; }
    [StringLength(50)]
    public string CodigoCIE10 { get; set; } = string.Empty;
    [StringLength(200)]
    public string Nombre { get; set; } = string.Empty;
    [StringLength(1000)]
    public string? Descripcion { get; set; }
    public bool Activo { get; set; }
}
