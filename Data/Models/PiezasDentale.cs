using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class PiezasDentale
{
    public short Id { get; set; }

    public short Codigo { get; set; }

    public string Tipo { get; set; } = null!;

    public virtual ICollection<OdontogramaDetalle> OdontogramaDetalles { get; set; } = new List<OdontogramaDetalle>();

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
