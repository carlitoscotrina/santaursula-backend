using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class vw_OdontogramaVigentePorPaciente
{
    public int PacienteId { get; set; }

    public int HistoriaClinicaId { get; set; }

    public short PiezaDentalId { get; set; }

    public byte? CaraDentalId { get; set; }

    public byte EstadoId { get; set; }

    public string? Observacion { get; set; }

    public DateTime FechaRegistro { get; set; }
}
