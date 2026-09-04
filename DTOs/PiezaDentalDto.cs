using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class PiezaDentalDto
{
    public short Id { get; set; }
    public short Codigo { get; set; }
    [StringLength(100)]
    public string Tipo { get; set; } = string.Empty;
}
