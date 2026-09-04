using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class ConfiguracionSistema
{
    public string Clave { get; set; } = null!;

    public string Valor { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateTime FechaModificacion { get; set; }
}
