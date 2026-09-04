using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class HistoriaDiagnostico
{
    public int Id { get; set; }

    public int HistoriaClinicaId { get; set; }

    public int DiagnosticoId { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual Diagnostico Diagnostico { get; set; } = null!;

    public virtual HistoriasClinica HistoriaClinica { get; set; } = null!;
}
