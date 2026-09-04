using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearHistoriaDiagnosticoDto
{
    [Required]
    public int HistoriaClinicaId { get; set; }

    [Required]
    public int DiagnosticoId { get; set; }

    [Required]
    [StringLength(50)]
    public string Tipo { get; set; } = string.Empty;
}
