using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Cita
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public DateOnly Fecha { get; set; }

    public TimeOnly Hora { get; set; }

    public string? Motivo { get; set; }

    public byte EstadoId { get; set; }

    public int? HistoriaClinicaId { get; set; }

    public int CreadoPorUsuarioId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario CreadoPorUsuario { get; set; } = null!;

    public virtual EstadosCitum Estado { get; set; } = null!;

    public virtual HistoriasClinica? HistoriaClinica { get; set; }

    public virtual Paciente Paciente { get; set; } = null!;
}
