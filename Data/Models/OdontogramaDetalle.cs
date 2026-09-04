using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class OdontogramaDetalle
{
    public int Id { get; set; }

    public int HistoriaClinicaId { get; set; }

    public short PiezaDentalId { get; set; }

    public byte? CaraDentalId { get; set; }

    public byte EstadoId { get; set; }

    public string? Observacion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual CarasDentale? CaraDental { get; set; }

    public virtual EstadosPiezaOdontograma Estado { get; set; } = null!;

    public virtual HistoriasClinica HistoriaClinica { get; set; } = null!;

    public virtual PiezasDentale PiezaDental { get; set; } = null!;
}
