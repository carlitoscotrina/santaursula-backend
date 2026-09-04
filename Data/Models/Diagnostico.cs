using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Diagnostico
{
    public int Id { get; set; }

    public string CodigoCIE10 { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<HistoriaDiagnostico> HistoriaDiagnosticos { get; set; } = new List<HistoriaDiagnostico>();
}
