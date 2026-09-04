using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class EvaluacionesPeriodontale
{
    public int Id { get; set; }

    public int HistoriaClinicaId { get; set; }

    public bool? PbBlanda { get; set; }

    public bool? PbDura { get; set; }

    public bool? CalculosInfragingivales { get; set; }

    public bool? Gingivitis { get; set; }

    public string? BolsaPeriodontal { get; set; }

    public string? Especificaciones { get; set; }

    public string? Pronostico { get; set; }

    public virtual HistoriasClinica HistoriaClinica { get; set; } = null!;
}
