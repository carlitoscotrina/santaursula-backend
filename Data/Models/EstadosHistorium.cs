using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class EstadosHistorium
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public virtual ICollection<HistoriasClinica> HistoriasClinicas { get; set; } = new List<HistoriasClinica>();
}
