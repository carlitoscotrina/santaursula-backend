using System;
using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class UsuarioDto
{
    public int Id { get; set; }
    [StringLength(50)]
    public string NombreUsuario { get; set; } = string.Empty;
    [StringLength(100)]
    public string NombreCompleto { get; set; } = string.Empty;
    public int RolId { get; set; }
    [StringLength(100)]
    public string? RolNombre { get; set; }
    [EmailAddress]
    [StringLength(150)]
    public string? Email { get; set; }
    public bool Activo { get; set; }
    public DateTime FechaCreacion { get; set; }
    public DateTime? UltimoAcceso { get; set; }
}
