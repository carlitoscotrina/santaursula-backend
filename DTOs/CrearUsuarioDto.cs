using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearUsuarioDto
{
    [Required]
    [StringLength(50)]
    public string NombreUsuario { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string NombreCompleto { get; set; } = string.Empty;

    [Required]
    public int RolId { get; set; }

    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }

    public bool Activo { get; set; } = true;
}
