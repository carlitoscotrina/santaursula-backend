using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Auditorium
{
    public long Id { get; set; }

    public int? UsuarioId { get; set; }

    public DateTime FechaHora { get; set; }

    public string Accion { get; set; } = null!;

    public string TablaAfectada { get; set; } = null!;

    public string RegistroAfectadoId { get; set; } = null!;

    public string? ValorAnterior { get; set; }

    public string? ValorNuevo { get; set; }

    public string? DireccionIP { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
