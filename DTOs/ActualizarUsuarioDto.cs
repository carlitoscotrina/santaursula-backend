using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class ActualizarUsuarioDto
{
    [StringLength(50)]
    public string? NombreUsuario { get; set; }
    [StringLength(100)]
    public string? NombreCompleto { get; set; }
    public int? RolId { get; set; }
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }
    public bool? Activo { get; set; }
    public DateTime? UltimoAcceso { get; set; }
}
