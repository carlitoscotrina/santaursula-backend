using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class MovimientosCuentum
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public int? HistoriaClinicaId { get; set; }

    public int? TratamientoId { get; set; }

    public byte TipoMovimientoId { get; set; }

    public DateOnly Fecha { get; set; }

    public string Concepto { get; set; } = null!;

    public decimal Debe { get; set; }

    public decimal Haber { get; set; }

    public int RegistradoPorUsuarioId { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual HistoriasClinica? HistoriaClinica { get; set; }

    public virtual Paciente Paciente { get; set; } = null!;

    public virtual Usuario RegistradoPorUsuario { get; set; } = null!;

    public virtual TiposMovimientoCuentum TipoMovimiento { get; set; } = null!;

    public virtual Tratamiento? Tratamiento { get; set; }
}
