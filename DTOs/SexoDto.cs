using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class SexoDto
{
    public byte Id { get; set; }
    [StringLength(50)]
    public string Nombre { get; set; } = string.Empty;
}
