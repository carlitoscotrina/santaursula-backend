using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class EstadosCitum
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();
}
