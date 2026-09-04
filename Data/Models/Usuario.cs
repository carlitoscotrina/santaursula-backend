using System;
using System.Collections.Generic;

namespace SantaUrsula.API.Data.Models;

public partial class Usuario
{
    public int Id { get; set; }

    public string NombreUsuario { get; set; } = null!;

    public string NombreCompleto { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RolId { get; set; }

    public string? Email { get; set; }

    public bool Activo { get; set; }

    public DateTime FechaCreacion { get; set; }

    public DateTime? UltimoAcceso { get; set; }

    public virtual ICollection<Auditorium> Auditoria { get; set; } = new List<Auditorium>();

    public virtual ICollection<Cita> Cita { get; set; } = new List<Cita>();

    public virtual ICollection<Documento> Documentos { get; set; } = new List<Documento>();

    public virtual ICollection<HistoriasClinica> HistoriasClinicaAnuladoPorUsuarios { get; set; } = new List<HistoriasClinica>();

    public virtual ICollection<HistoriasClinica> HistoriasClinicaProfesionals { get; set; } = new List<HistoriasClinica>();

    public virtual ICollection<MovimientosCuentum> MovimientosCuenta { get; set; } = new List<MovimientosCuentum>();

    public virtual Role Rol { get; set; } = null!;
}
