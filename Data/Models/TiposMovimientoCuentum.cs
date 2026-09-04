using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class TiposMovimientoCuentum
{
    public byte Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Naturaleza { get; set; } = null!;

    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();
}
