using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class PermisoDto
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Modulo { get; set; } = string.Empty;
    [StringLength(100)]
    public string Accion { get; set; } = string.Empty;
}
