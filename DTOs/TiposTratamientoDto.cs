using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class TiposTratamientoDto
{
    public int Id { get; set; }
    [StringLength(200)]
    public string Nombre { get; set; } = string.Empty;
    public bool RequierePieza { get; set; }
    public bool RequiereCara { get; set; }
    public decimal? PrecioReferencial { get; set; }
    public bool Activo { get; set; }
}
