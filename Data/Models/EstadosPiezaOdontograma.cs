using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class EstadosPiezaOdontograma
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<OdontogramaDetalle> OdontogramaDetalles { get; set; } = new List<OdontogramaDetalle>();
}
