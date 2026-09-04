using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class RoleDto
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Nombre { get; set; } = string.Empty;
}
