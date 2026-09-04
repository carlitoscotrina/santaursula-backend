using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class TiposTratamiento
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public bool RequierePieza { get; set; }

    public bool RequiereCara { get; set; }

    public decimal? PrecioReferencial { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
