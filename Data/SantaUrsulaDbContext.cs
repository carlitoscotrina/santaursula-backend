using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data.Models;

namespace SantaUrsula.API.Data;

public partial class SantaUrsulaDbContext : DbContext
{
    public SantaUrsulaDbContext(DbContextOptions<SantaUrsulaDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Auditorium> Auditoria { get; set; }

    public virtual DbSet<CarasDentale> CarasDentales { get; set; }

    public virtual DbSet<Cita> Citas { get; set; }

    public virtual DbSet<ConfiguracionSistema> ConfiguracionSistemas { get; set; }

    public virtual DbSet<Diagnostico> Diagnosticos { get; set; }

    public virtual DbSet<Documento> Documentos { get; set; }

    public virtual DbSet<EstadosCitum> EstadosCita { get; set; }

    public virtual DbSet<EstadosHistorium> EstadosHistoria { get; set; }

    public virtual DbSet<EstadosPiezaOdontograma> EstadosPiezaOdontogramas { get; set; }

    public virtual DbSet<EvaluacionesPeriodontale> EvaluacionesPeriodontales { get; set; }

    public virtual DbSet<HistoriaDiagnostico> HistoriaDiagnosticos { get; set; }

    public virtual DbSet<HistoriasClinica> HistoriasClinicas { get; set; }

    public virtual DbSet<MovimientosCuentum> MovimientosCuenta { get; set; }

    public virtual DbSet<OdontogramaDetalle> OdontogramaDetalles { get; set; }

    public virtual DbSet<Paciente> Pacientes { get; set; }

    public virtual DbSet<Permiso> Permisos { get; set; }

    public virtual DbSet<PiezasDentale> PiezasDentales { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    public virtual DbSet<TiposMovimientoCuentum> TiposMovimientoCuenta { get; set; }

    public virtual DbSet<TiposTratamiento> TiposTratamientos { get; set; }

    public virtual DbSet<Tratamiento> Tratamientos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<vw_OdontogramaVigentePorPaciente> vw_OdontogramaVigentePorPacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Auditorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Auditori__3214EC079A4B9780");

            entity.HasIndex(e => new { e.TablaAfectada, e.RegistroAfectadoId }, "IX_Auditoria_Tabla_Registro");

            entity.HasIndex(e => new { e.UsuarioId, e.FechaHora }, "IX_Auditoria_Usuario_Fecha");

            entity.Property(e => e.Accion).HasMaxLength(20);
            entity.Property(e => e.DireccionIP).HasMaxLength(45);
            entity.Property(e => e.FechaHora).HasDefaultValueSql("now()");
            entity.Property(e => e.RegistroAfectadoId).HasMaxLength(50);
            entity.Property(e => e.TablaAfectada).HasMaxLength(50);

            entity.HasOne(d => d.Usuario).WithMany(p => p.Auditoria)
                .HasForeignKey(d => d.UsuarioId)
                .HasConstraintName("FK_Auditoria_Usuario");
        });

        modelBuilder.Entity<CarasDentale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CarasDen__3214EC07607CAE4E");

            entity.HasIndex(e => e.Nombre, "UQ_Caras_Nombre").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).HasMaxLength(20);
        });

        modelBuilder.Entity<Cita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Citas__3214EC075D2DB155");

            entity.HasIndex(e => new { e.Fecha, e.Hora }, "IX_Citas_Fecha");

            entity.HasIndex(e => new { e.PacienteId, e.Fecha }, "IX_Citas_PacienteId");

            entity.Property(e => e.EstadoId).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("now()");
            entity.Property(e => e.Motivo).HasMaxLength(300);

            entity.HasOne(d => d.CreadoPorUsuario).WithMany(p => p.Cita)
                .HasForeignKey(d => d.CreadoPorUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_CreadoPor");

            entity.HasOne(d => d.Estado).WithMany(p => p.Cita)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Estado");

            entity.HasOne(d => d.HistoriaClinica).WithMany(p => p.Cita)
                .HasForeignKey(d => d.HistoriaClinicaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Citas_Historia");

            entity.HasOne(d => d.Paciente).WithMany(p => p.Cita)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Citas_Paciente");
        });

        modelBuilder.Entity<ConfiguracionSistema>(entity =>
        {
            entity.HasKey(e => e.Clave).HasName("PK__Configur__E8181E10CA72FCD3");

            entity.ToTable("ConfiguracionSistema");

            entity.Property(e => e.Clave).HasMaxLength(50);
            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.FechaModificacion).HasDefaultValueSql("now()");
            entity.Property(e => e.Valor).HasMaxLength(400);
        });

        modelBuilder.Entity<Diagnostico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Diagnost__3214EC07CB16B9FE");

            entity.HasIndex(e => e.Nombre, "IX_Diagnosticos_Nombre");

            entity.HasIndex(e => e.CodigoCIE10, "UQ_Diagnosticos_CIE10").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.CodigoCIE10).HasMaxLength(10);
            entity.Property(e => e.Descripcion).HasMaxLength(500);
            entity.Property(e => e.Nombre).HasMaxLength(200);
        });

        modelBuilder.Entity<Documento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC077C29138B");

            entity.HasIndex(e => e.HistoriaClinicaId, "IX_Documentos_HistoriaClinicaId");

            entity.Property(e => e.FechaGeneracion).HasDefaultValueSql("now()");
            entity.Property(e => e.RutaArchivo).HasMaxLength(400);
            entity.Property(e => e.TipoDocumento)
                .HasMaxLength(30)
                .HasDefaultValue("HistoriaClinicaPDF");
            entity.Property(e => e.Version).HasDefaultValue(1);

            entity.HasOne(d => d.GeneradoPorUsuario).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.GeneradoPorUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Documentos_Usuario");

            entity.HasOne(d => d.HistoriaClinica).WithMany(p => p.Documentos)
                .HasForeignKey(d => d.HistoriaClinicaId)
                .HasConstraintName("FK_Documentos_Historia");
        });

        modelBuilder.Entity<EstadosCitum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadosC__3214EC07D5AD3083");

            entity.HasIndex(e => e.Nombre, "UQ_EstadosCita_Nombre").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).HasMaxLength(20);
        });

        modelBuilder.Entity<EstadosHistorium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadosH__3214EC07D567E55F");

            entity.HasIndex(e => e.Nombre, "UQ_EstadosHistoria_Nombre").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).HasMaxLength(20);
        });

        modelBuilder.Entity<EstadosPiezaOdontograma>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EstadosP__3214EC07D9ABFC74");

            entity.ToTable("EstadosPiezaOdontograma");

            entity.HasIndex(e => e.Nombre, "UQ_EstadosPieza_Nombre").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<EvaluacionesPeriodontale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Evaluaci__3214EC075DCF469C");

            entity.HasIndex(e => e.HistoriaClinicaId, "UQ_EvalPeriodontal_Historia").IsUnique();

            entity.Property(e => e.BolsaPeriodontal).HasMaxLength(50);
            entity.Property(e => e.Especificaciones).HasMaxLength(500);
            entity.Property(e => e.Pronostico).HasMaxLength(200);

            entity.HasOne(d => d.HistoriaClinica).WithOne(p => p.EvaluacionesPeriodontale)
                .HasForeignKey<EvaluacionesPeriodontale>(d => d.HistoriaClinicaId)
                .HasConstraintName("FK_EvalPeriodontal_Historia");
        });

        modelBuilder.Entity<HistoriaDiagnostico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC078BA3A1B9");

            entity.HasIndex(e => e.DiagnosticoId, "IX_HistoriaDiagnosticos_DiagnosticoId");

            entity.HasIndex(e => new { e.HistoriaClinicaId, e.DiagnosticoId, e.Tipo }, "UQ_HistoriaDiag").IsUnique();

            entity.Property(e => e.Tipo).HasMaxLength(15);

            entity.HasOne(d => d.Diagnostico).WithMany(p => p.HistoriaDiagnosticos)
                .HasForeignKey(d => d.DiagnosticoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_HistoriaDiag_Diagnostico");

            entity.HasOne(d => d.HistoriaClinica).WithMany(p => p.HistoriaDiagnosticos)
                .HasForeignKey(d => d.HistoriaClinicaId)
                .HasConstraintName("FK_HistoriaDiag_Historia");
        });

        modelBuilder.Entity<HistoriasClinica>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Historia__3214EC07C2A21A98");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_HistoriasClinicas_Auditoria");
                    tb.HasTrigger("trg_HistoriasClinicas_ProtegerEstado");
                });

            entity.HasIndex(e => new { e.PacienteId, e.FechaHoraAtencion }, "IX_Historias_PacienteId").IsDescending(false, true);

            entity.HasIndex(e => e.NumeroHistoria, "UQ_Historias_NumeroHistoria")
                .IsUnique()
                .HasFilter("(\"NumeroHistoria\" IS NOT NULL)");

            entity.Property(e => e.AntecedentesFamiliares).HasMaxLength(500);
            entity.Property(e => e.AntecedentesPatologicos).HasMaxLength(500);
            entity.Property(e => e.EstadoId).HasDefaultValue((byte)1);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("now()");
            entity.Property(e => e.Gestante).HasMaxLength(10);
            entity.Property(e => e.MedicacionActual).HasMaxLength(500);
            entity.Property(e => e.MotivoAnulacion).HasMaxLength(300);
            entity.Property(e => e.MotivoConsulta).HasMaxLength(500);
            entity.Property(e => e.NumeroHistoria).HasMaxLength(20);
            entity.Property(e => e.TiempoEnfermedad).HasMaxLength(200);

            entity.HasOne(d => d.AnuladoPorUsuario).WithMany(p => p.HistoriasClinicaAnuladoPorUsuarios)
                .HasForeignKey(d => d.AnuladoPorUsuarioId)
                .HasConstraintName("FK_Historias_AnuladoPor");

            entity.HasOne(d => d.AtencionCorrige).WithMany(p => p.InverseAtencionCorrige)
                .HasForeignKey(d => d.AtencionCorrigeId)
                .HasConstraintName("FK_Historias_Corrige");

            entity.HasOne(d => d.Estado).WithMany(p => p.HistoriasClinicas)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historias_Estado");

            entity.HasOne(d => d.Paciente).WithMany(p => p.HistoriasClinicas)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historias_Pacientes");

            entity.HasOne(d => d.Profesional).WithMany(p => p.HistoriasClinicaProfesionals)
                .HasForeignKey(d => d.ProfesionalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Historias_Profesional");
        });

        modelBuilder.Entity<MovimientosCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movimien__3214EC07A7F0B971");

            entity.ToTable(tb =>
                {
                    tb.HasTrigger("trg_MovimientosCuenta_Auditoria");
                    tb.HasTrigger("trg_MovimientosCuenta_ValidarNaturaleza");
                });

            entity.HasIndex(e => new { e.PacienteId, e.Fecha, e.Id }, "IX_MovimientosCuenta_Paciente_Fecha");

            entity.Property(e => e.Concepto).HasMaxLength(200);
            entity.Property(e => e.Debe).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("now()");
            entity.Property(e => e.Haber).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.HistoriaClinica).WithMany(p => p.MovimientosCuenta)
                .HasForeignKey(d => d.HistoriaClinicaId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Movimientos_Historia");

            entity.HasOne(d => d.Paciente).WithMany(p => p.MovimientosCuenta)
                .HasForeignKey(d => d.PacienteId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Paciente");

            entity.HasOne(d => d.RegistradoPorUsuario).WithMany(p => p.MovimientosCuenta)
                .HasForeignKey(d => d.RegistradoPorUsuarioId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Usuario");

            entity.HasOne(d => d.TipoMovimiento).WithMany(p => p.MovimientosCuenta)
                .HasForeignKey(d => d.TipoMovimientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Movimientos_Tipo");

            entity.HasOne(d => d.Tratamiento).WithMany(p => p.MovimientosCuenta)
                .HasForeignKey(d => d.TratamientoId)
                .HasConstraintName("FK_Movimientos_Tratamiento");
        });

        modelBuilder.Entity<OdontogramaDetalle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Odontogr__3214EC07FE25FD69");

            entity.ToTable("OdontogramaDetalle");

            entity.HasIndex(e => e.HistoriaClinicaId, "IX_Odontograma_HistoriaClinicaId");

            entity.HasIndex(e => new { e.PiezaDentalId, e.FechaRegistro }, "IX_Odontograma_Pieza").IsDescending(false, true);

            entity.HasIndex(e => new { e.HistoriaClinicaId, e.PiezaDentalId, e.CaraDentalId }, "UQ_Odontograma_Historia_Pieza_Cara")
                .IsUnique()
                .HasFilter("(\"CaraDentalId\" IS NOT NULL)");

            entity.HasIndex(e => new { e.HistoriaClinicaId, e.PiezaDentalId }, "UQ_Odontograma_Historia_Pieza_SinCara")
                .IsUnique()
                .HasFilter("(\"CaraDentalId\" IS NULL)");

            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("now()");
            entity.Property(e => e.Observacion).HasMaxLength(300);

            entity.HasOne(d => d.CaraDental).WithMany(p => p.OdontogramaDetalles)
                .HasForeignKey(d => d.CaraDentalId)
                .HasConstraintName("FK_Odontograma_Cara");

            entity.HasOne(d => d.Estado).WithMany(p => p.OdontogramaDetalles)
                .HasForeignKey(d => d.EstadoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Odontograma_Estado");

            entity.HasOne(d => d.HistoriaClinica).WithMany(p => p.OdontogramaDetalles)
                .HasForeignKey(d => d.HistoriaClinicaId)
                .HasConstraintName("FK_Odontograma_Historia");

            entity.HasOne(d => d.PiezaDental).WithMany(p => p.OdontogramaDetalles)
                .HasForeignKey(d => d.PiezaDentalId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Odontograma_Pieza");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Paciente__3214EC07D540C251");

            entity.HasIndex(e => new { e.ApellidoPaterno, e.ApellidoMaterno, e.Nombres }, "IX_Pacientes_Apellidos");

            entity.HasIndex(e => e.DNI, "UQ_Pacientes_DNI").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Alergias).HasMaxLength(500);
            entity.Property(e => e.ApellidoMaterno).HasMaxLength(100);
            entity.Property(e => e.ApellidoPaterno).HasMaxLength(100);
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.CelularAcompanante).HasMaxLength(20);
            entity.Property(e => e.DNI).HasMaxLength(15);
            entity.Property(e => e.DomicilioActual).HasMaxLength(250);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EstadoCivil).HasMaxLength(20);
            entity.Property(e => e.FechaRegistro).HasDefaultValueSql("now()");
            entity.Property(e => e.LugarNacimiento).HasMaxLength(150);
            entity.Property(e => e.LugarProcedencia).HasMaxLength(150);
            entity.Property(e => e.NombreAcompanante).HasMaxLength(150);
            entity.Property(e => e.Nombres).HasMaxLength(100);
            entity.Property(e => e.Ocupacion).HasMaxLength(100);
            entity.Property(e => e.Religion).HasMaxLength(50);

            entity.HasOne(d => d.Sexo).WithMany(p => p.Pacientes)
                .HasForeignKey(d => d.SexoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pacientes_Sexo");
        });

        modelBuilder.Entity<Permiso>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Permisos__3214EC073E8676F1");

            entity.HasIndex(e => new { e.Modulo, e.Accion }, "UQ_Permisos_Modulo_Accion").IsUnique();

            entity.Property(e => e.Accion).HasMaxLength(20);
            entity.Property(e => e.Modulo).HasMaxLength(50);
        });

        modelBuilder.Entity<PiezasDentale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PiezasDe__3214EC071EBD3F5A");

            entity.HasIndex(e => e.Codigo, "UQ_Piezas_Codigo").IsUnique();

            entity.Property(e => e.Tipo).HasMaxLength(20);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC07E4215054");

            entity.HasIndex(e => e.Nombre, "UQ_Roles_Nombre").IsUnique();

            entity.Property(e => e.Descripcion).HasMaxLength(200);
            entity.Property(e => e.Nombre).HasMaxLength(50);

            entity.HasMany(d => d.Permisos).WithMany(p => p.Rols)
                .UsingEntity<Dictionary<string, object>>(
                    "RolPermiso",
                    r => r.HasOne<Permiso>().WithMany()
                        .HasForeignKey("PermisoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolPermisos_Permisos"),
                    l => l.HasOne<Role>().WithMany()
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RolPermisos_Roles"),
                    j =>
                    {
                        j.HasKey("RolId", "PermisoId");
                        j.ToTable("RolPermisos");
                    });
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Sexos__3214EC070097A34C");

            entity.HasIndex(e => e.Nombre, "UQ_Sexos_Nombre").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<TiposMovimientoCuentum>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposMov__3214EC0709934156");

            entity.HasIndex(e => e.Nombre, "UQ_TiposMovimiento_Nombre").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Naturaleza)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Nombre).HasMaxLength(30);
        });

        modelBuilder.Entity<TiposTratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TiposTra__3214EC074E63E24E");

            entity.ToTable("TiposTratamiento");

            entity.HasIndex(e => e.Nombre, "UQ_TiposTratamiento_Nombre").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Nombre).HasMaxLength(150);
            entity.Property(e => e.PrecioReferencial).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RequierePieza).HasDefaultValue(true);
        });

        modelBuilder.Entity<Tratamiento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Tratamie__3214EC07761047BC");

            entity.HasIndex(e => e.HistoriaClinicaId, "IX_Tratamientos_HistoriaClinicaId");

            entity.HasIndex(e => e.TipoTratamientoId, "IX_Tratamientos_TipoTratamientoId");

            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .HasDefaultValue("Planificado");
            entity.Property(e => e.FechaAplicacion).HasDefaultValueSql("CURRENT_DATE");
            entity.Property(e => e.Observacion).HasMaxLength(300);
            entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.HistoriaClinica).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.HistoriaClinicaId)
                .HasConstraintName("FK_Tratamientos_Historia");

            entity.HasOne(d => d.PiezaDental).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.PiezaDentalId)
                .HasConstraintName("FK_Tratamientos_Pieza");

            entity.HasOne(d => d.TipoTratamiento).WithMany(p => p.Tratamientos)
                .HasForeignKey(d => d.TipoTratamientoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Tratamientos_Tipo");

            entity.HasMany(d => d.CaraDentals).WithMany(p => p.Tratamientos)
                .UsingEntity<Dictionary<string, object>>(
                    "TratamientoCara",
                    r => r.HasOne<CarasDentale>().WithMany()
                        .HasForeignKey("CaraDentalId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_TratamientoCaras_Cara"),
                    l => l.HasOne<Tratamiento>().WithMany()
                        .HasForeignKey("TratamientoId")
                        .HasConstraintName("FK_TratamientoCaras_Tratamiento"),
                    j =>
                    {
                        j.HasKey("TratamientoId", "CaraDentalId");
                        j.ToTable("TratamientoCaras");
                    });
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Usuarios__3214EC079714764D");

            entity.HasIndex(e => e.Email, "UQ_Usuarios_Email")
                .IsUnique()
                .HasFilter("(\"Email\" IS NOT NULL)");

            entity.HasIndex(e => e.NombreUsuario, "UQ_Usuarios_NombreUsuario").IsUnique();

            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.FechaCreacion).HasDefaultValueSql("now()");
            entity.Property(e => e.NombreCompleto).HasMaxLength(150);
            entity.Property(e => e.NombreUsuario).HasMaxLength(50);
            entity.Property(e => e.PasswordHash).HasMaxLength(256);

            entity.HasOne(d => d.Rol).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.RolId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Usuarios_Roles");
        });

        modelBuilder.Entity<vw_OdontogramaVigentePorPaciente>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vw_OdontogramaVigentePorPaciente");

            entity.Property(e => e.Observacion).HasMaxLength(300);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
