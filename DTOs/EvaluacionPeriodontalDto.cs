using System.ComponentModel.DataAnnotations;

namespace SantaUrsula.API.DTOs;

public class EvaluacionPeriodontalDto
{
    public int Id { get; set; }
    public int HistoriaClinicaId { get; set; }
    public bool? PbBlanda { get; set; }
    public bool? PbDura { get; set; }
    public bool? CalculosInfragingivales { get; set; }
    public bool? Gingivitis { get; set; }
    [StringLength(100)]
    public string? BolsaPeriodontal { get; set; }
    [StringLength(500)]
    public string? Especificaciones { get; set; }
    [StringLength(200)]
    public string? Pronostico { get; set; }
}
