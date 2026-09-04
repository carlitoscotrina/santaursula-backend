using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Documento
{
    public int Id { get; set; }

    public int HistoriaClinicaId { get; set; }

    public string TipoDocumento { get; set; } = null!;

    public string RutaArchivo { get; set; } = null!;

    public int Version { get; set; }

    public DateTime FechaGeneracion { get; set; }

    public int GeneradoPorUsuarioId { get; set; }

    public virtual Usuario GeneradoPorUsuario { get; set; } = null!;

    public virtual HistoriasClinica HistoriaClinica { get; set; } = null!;
}
