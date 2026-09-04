namespace SantaUrsula.API.DTOs;

public class HistoriaDiagnosticoDto
{
    public int Id { get; set; }
    public int HistoriaClinicaId { get; set; }
    public int DiagnosticoId { get; set; }
    public string DiagnosticoCodigo { get; set; } = string.Empty;
    public string DiagnosticoNombre { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
}
