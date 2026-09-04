using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class DocumentoDto
{
    public int Id { get; set; }
    public int HistoriaClinicaId { get; set; }
    [StringLength(100)]
    public string TipoDocumento { get; set; } = string.Empty;
    [StringLength(500)]
    public string RutaArchivo { get; set; } = string.Empty;
    public int Version { get; set; }
    public DateTime FechaGeneracion { get; set; }
    public int GeneradoPorUsuarioId { get; set; }
    [StringLength(200)]
    public string GeneradoPorUsuarioNombre { get; set; } = string.Empty;
}
