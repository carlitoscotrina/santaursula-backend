using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Paciente
{
    public int Id { get; set; }

    public string DNI { get; set; } = null!;

    public string Nombres { get; set; } = null!;

    public string ApellidoPaterno { get; set; } = null!;

    public string? ApellidoMaterno { get; set; }

    public DateOnly FechaNacimiento { get; set; }

    public byte SexoId { get; set; }

    public string? Ocupacion { get; set; }

    public string? Religion { get; set; }

    public string? EstadoCivil { get; set; }

    public string? LugarNacimiento { get; set; }

    public string? LugarProcedencia { get; set; }

    public string? DomicilioActual { get; set; }

    public string? Celular { get; set; }

    public string? Email { get; set; }

    public string? Alergias { get; set; }

    public string? NombreAcompanante { get; set; }

    public string? CelularAcompanante { get; set; }

    public DateTime FechaRegistro { get; set; }

    public bool Activo { get; set; }

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<HistoriasClinica> HistoriasClinicas { get; set; } = new List<HistoriasClinica>();

    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();

    public virtual Sexo Sexo { get; set; } = null!;
}
