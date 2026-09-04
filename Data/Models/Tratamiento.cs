using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Tratamiento
{
    public int Id { get; set; }

    public int HistoriaClinicaId { get; set; }

    public int TipoTratamientoId { get; set; }

    public short? PiezaDentalId { get; set; }

    public decimal Precio { get; set; }

    public string? Observacion { get; set; }

    public DateOnly FechaAplicacion { get; set; }

    public string Estado { get; set; } = null!;

    public virtual HistoriasClinica HistoriaClinica { get; set; } = null!;

    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();

    public virtual PiezasDentale? PiezaDental { get; set; }

    public virtual TiposTratamiento TipoTratamiento { get; set; } = null!;

    public virtual ICollection<CarasDentale> CaraDentals { get; set; } = new List<CarasDentale>();
}
