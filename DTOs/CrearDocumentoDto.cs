using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class CrearDocumentoDto
{
    [Required]
    public int HistoriaClinicaId { get; set; }

    [Required]
    [StringLength(100)]
    public string TipoDocumento { get; set; } = string.Empty;

    [Required]
    [StringLength(500)]
    public string RutaArchivo { get; set; } = string.Empty;

    public int Version { get; set; } = 1;
    [Required]
    public int GeneradoPorUsuarioId { get; set; }
}
