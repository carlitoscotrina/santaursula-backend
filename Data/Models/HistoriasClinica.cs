using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class HistoriasClinica
{
    public int Id { get; set; }

    public int PacienteId { get; set; }

    public string? NumeroHistoria { get; set; }

    public DateTime FechaHoraAtencion { get; set; }

    public int ProfesionalId { get; set; }

    public string? MotivoConsulta { get; set; }

    public bool? Dolor { get; set; }

    public string? TiempoEnfermedad { get; set; }

    public string? AntecedentesPatologicos { get; set; }

    public string? AntecedentesFamiliares { get; set; }

    public string? MedicacionActual { get; set; }

    public bool? ExtraccionesPrevias { get; set; }

    public bool? ProblemasPostExtraccion { get; set; }

    public bool? HemorragiaExcesiva { get; set; }

    public DateOnly? UltimaVisitaOdontologo { get; set; }

    public string? Gestante { get; set; }

    public string? Observaciones { get; set; }

    public byte EstadoId { get; set; }

    public DateTime? FechaCierre { get; set; }

    public string? MotivoAnulacion { get; set; }

    public int? AnuladoPorUsuarioId { get; set; }

    public DateTime? FechaAnulacion { get; set; }

    public int? AtencionCorrigeId { get; set; }

    public DateTime FechaCreacion { get; set; }

    public virtual Usuario? AnuladoPorUsuario { get; set; }

    public virtual HistoriasClinica? AtencionCorrige { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual EstadosHistorium Estado { get; set; } = null!;

    public virtual EvaluacionesPeriodontale? EvaluacionesPeriodontale { get; set; }

    public virtual ICollection<HistoriaDiagnostico> HistoriaDiagnosticos { get; set; } = new List<HistoriaDiagnostico>();

    public virtual ICollection<HistoriasClinica> InverseAtencionCorrige { get; set; } = new List<HistoriasClinica>();

    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();

    public virtual ICollection<OdontogramaDetalle> OdontogramaDetalles { get; set; } = new List<OdontogramaDetalle>();

    public virtual Paciente Paciente { get; set; } = null!;

    public virtual Usuario Profesional { get; set; } = null!;

    public virtual ICollection<Tratamiento> Tratamientos { get; set; } = new List<Tratamiento>();
}
