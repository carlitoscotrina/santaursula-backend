using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SantaUrsula.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarasDentales",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CarasDen__3214EC07607CAE4E", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfiguracionSistema",
                columns: table => new
                {
                    Clave = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Valor = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    FechaModificacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Configur__E8181E10CA72FCD3", x => x.Clave);
                });

            migrationBuilder.CreateTable(
                name: "Diagnosticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CodigoCIE10 = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Diagnost__3214EC07CB16B9FE", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosCita",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadosC__3214EC07D5AD3083", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosHistoria",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadosH__3214EC07D567E55F", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EstadosPiezaOdontograma",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__EstadosP__3214EC07D9ABFC74", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Modulo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Accion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Permisos__3214EC073E8676F1", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PiezasDentales",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<short>(type: "smallint", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PiezasDe__3214EC071EBD3F5A", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__3214EC07E4215054", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sexos",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Sexos__3214EC070097A34C", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposMovimientoCuenta",
                columns: table => new
                {
                    Id = table.Column<byte>(type: "smallint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Naturaleza = table.Column<string>(type: "character(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TiposMov__3214EC0709934156", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TiposTratamiento",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    RequierePieza = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    RequiereCara = table.Column<bool>(type: "boolean", nullable: false),
                    PrecioReferencial = table.Column<decimal>(type: "numeric(10,2)", nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TiposTra__3214EC074E63E24E", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolPermisos",
                columns: table => new
                {
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    PermisoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermisos", x => new { x.RolId, x.PermisoId });
                    table.ForeignKey(
                        name: "FK_RolPermisos_Permisos",
                        column: x => x.PermisoId,
                        principalTable: "Permisos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RolPermisos_Roles",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreUsuario = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NombreCompleto = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    RolId = table.Column<int>(type: "integer", nullable: false),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    UltimoAcceso = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Usuarios__3214EC079714764D", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Pacientes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DNI = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    Nombres = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApellidoPaterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApellidoMaterno = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    FechaNacimiento = table.Column<DateOnly>(type: "date", nullable: false),
                    SexoId = table.Column<byte>(type: "smallint", nullable: false),
                    Ocupacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Religion = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    EstadoCivil = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    LugarNacimiento = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    LugarProcedencia = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    DomicilioActual = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Celular = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    Alergias = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    NombreAcompanante = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: true),
                    CelularAcompanante = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Activo = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Paciente__3214EC07D540C251", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pacientes_Sexo",
                        column: x => x.SexoId,
                        principalTable: "Sexos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Auditoria",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true),
                    FechaHora = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    Accion = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    TablaAfectada = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    RegistroAfectadoId = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    ValorAnterior = table.Column<string>(type: "text", nullable: true),
                    ValorNuevo = table.Column<string>(type: "text", nullable: true),
                    DireccionIP = table.Column<string>(type: "character varying(45)", maxLength: 45, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Auditori__3214EC079A4B9780", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Auditoria_Usuario",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "HistoriasClinicas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    NumeroHistoria = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    FechaHoraAtencion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ProfesionalId = table.Column<int>(type: "integer", nullable: false),
                    MotivoConsulta = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Dolor = table.Column<bool>(type: "boolean", nullable: true),
                    TiempoEnfermedad = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    AntecedentesPatologicos = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AntecedentesFamiliares = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MedicacionActual = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    ExtraccionesPrevias = table.Column<bool>(type: "boolean", nullable: true),
                    ProblemasPostExtraccion = table.Column<bool>(type: "boolean", nullable: true),
                    HemorragiaExcesiva = table.Column<bool>(type: "boolean", nullable: true),
                    UltimaVisitaOdontologo = table.Column<DateOnly>(type: "date", nullable: true),
                    Gestante = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Observaciones = table.Column<string>(type: "text", nullable: true),
                    EstadoId = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1),
                    FechaCierre = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    MotivoAnulacion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    AnuladoPorUsuarioId = table.Column<int>(type: "integer", nullable: true),
                    FechaAnulacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    AtencionCorrigeId = table.Column<int>(type: "integer", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__3214EC07C2A21A98", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Historias_AnuladoPor",
                        column: x => x.AnuladoPorUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Historias_Corrige",
                        column: x => x.AtencionCorrigeId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Historias_Estado",
                        column: x => x.EstadoId,
                        principalTable: "EstadosHistoria",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Historias_Pacientes",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Historias_Profesional",
                        column: x => x.ProfesionalId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Citas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Hora = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Motivo = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    EstadoId = table.Column<byte>(type: "smallint", nullable: false, defaultValue: (byte)1),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: true),
                    CreadoPorUsuarioId = table.Column<int>(type: "integer", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Citas__3214EC075D2DB155", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Citas_CreadoPor",
                        column: x => x.CreadoPorUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Citas_Estado",
                        column: x => x.EstadoId,
                        principalTable: "EstadosCita",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Citas_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Citas_Paciente",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: false),
                    TipoDocumento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false, defaultValue: "HistoriaClinicaPDF"),
                    RutaArchivo = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false, defaultValue: 1),
                    FechaGeneracion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()"),
                    GeneradoPorUsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Document__3214EC077C29138B", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documentos_Usuario",
                        column: x => x.GeneradoPorUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EvaluacionesPeriodontales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: false),
                    PbBlanda = table.Column<bool>(type: "boolean", nullable: true),
                    PbDura = table.Column<bool>(type: "boolean", nullable: true),
                    CalculosInfragingivales = table.Column<bool>(type: "boolean", nullable: true),
                    Gingivitis = table.Column<bool>(type: "boolean", nullable: true),
                    BolsaPeriodontal = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Especificaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Pronostico = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Evaluaci__3214EC075DCF469C", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvalPeriodontal_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoriaDiagnosticos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: false),
                    DiagnosticoId = table.Column<int>(type: "integer", nullable: false),
                    Tipo = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Historia__3214EC078BA3A1B9", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoriaDiag_Diagnostico",
                        column: x => x.DiagnosticoId,
                        principalTable: "Diagnosticos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoriaDiag_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OdontogramaDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: false),
                    PiezaDentalId = table.Column<short>(type: "smallint", nullable: false),
                    CaraDentalId = table.Column<byte>(type: "smallint", nullable: true),
                    EstadoId = table.Column<byte>(type: "smallint", nullable: false),
                    Observacion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Odontogr__3214EC07FE25FD69", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Odontograma_Cara",
                        column: x => x.CaraDentalId,
                        principalTable: "CarasDentales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Odontograma_Estado",
                        column: x => x.EstadoId,
                        principalTable: "EstadosPiezaOdontograma",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Odontograma_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Odontograma_Pieza",
                        column: x => x.PiezaDentalId,
                        principalTable: "PiezasDentales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Tratamientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: false),
                    TipoTratamientoId = table.Column<int>(type: "integer", nullable: false),
                    PiezaDentalId = table.Column<short>(type: "smallint", nullable: true),
                    Precio = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Observacion = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    FechaAplicacion = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    Estado = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false, defaultValue: "Planificado")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Tratamie__3214EC07761047BC", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tratamientos_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tratamientos_Pieza",
                        column: x => x.PiezaDentalId,
                        principalTable: "PiezasDentales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tratamientos_Tipo",
                        column: x => x.TipoTratamientoId,
                        principalTable: "TiposTratamiento",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MovimientosCuenta",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PacienteId = table.Column<int>(type: "integer", nullable: false),
                    HistoriaClinicaId = table.Column<int>(type: "integer", nullable: true),
                    TratamientoId = table.Column<int>(type: "integer", nullable: true),
                    TipoMovimientoId = table.Column<byte>(type: "smallint", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Concepto = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Debe = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Haber = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    RegistradoPorUsuarioId = table.Column<int>(type: "integer", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "now()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Movimien__3214EC07A7F0B971", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Historia",
                        column: x => x.HistoriaClinicaId,
                        principalTable: "HistoriasClinicas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Movimientos_Paciente",
                        column: x => x.PacienteId,
                        principalTable: "Pacientes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimientos_Tipo",
                        column: x => x.TipoMovimientoId,
                        principalTable: "TiposMovimientoCuenta",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimientos_Tratamiento",
                        column: x => x.TratamientoId,
                        principalTable: "Tratamientos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Movimientos_Usuario",
                        column: x => x.RegistradoPorUsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TratamientoCaras",
                columns: table => new
                {
                    TratamientoId = table.Column<int>(type: "integer", nullable: false),
                    CaraDentalId = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TratamientoCaras", x => new { x.TratamientoId, x.CaraDentalId });
                    table.ForeignKey(
                        name: "FK_TratamientoCaras_Cara",
                        column: x => x.CaraDentalId,
                        principalTable: "CarasDentales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TratamientoCaras_Tratamiento",
                        column: x => x.TratamientoId,
                        principalTable: "Tratamientos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_Tabla_Registro",
                table: "Auditoria",
                columns: new[] { "TablaAfectada", "RegistroAfectadoId" });

            migrationBuilder.CreateIndex(
                name: "IX_Auditoria_Usuario_Fecha",
                table: "Auditoria",
                columns: new[] { "UsuarioId", "FechaHora" });

            migrationBuilder.CreateIndex(
                name: "UQ_Caras_Nombre",
                table: "CarasDentales",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Citas_CreadoPorUsuarioId",
                table: "Citas",
                column: "CreadoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_EstadoId",
                table: "Citas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_Fecha",
                table: "Citas",
                columns: new[] { "Fecha", "Hora" });

            migrationBuilder.CreateIndex(
                name: "IX_Citas_HistoriaClinicaId",
                table: "Citas",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Citas_PacienteId",
                table: "Citas",
                columns: new[] { "PacienteId", "Fecha" });

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosticos_Nombre",
                table: "Diagnosticos",
                column: "Nombre");

            migrationBuilder.CreateIndex(
                name: "UQ_Diagnosticos_CIE10",
                table: "Diagnosticos",
                column: "CodigoCIE10",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_GeneradoPorUsuarioId",
                table: "Documentos",
                column: "GeneradoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_HistoriaClinicaId",
                table: "Documentos",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "UQ_EstadosCita_Nombre",
                table: "EstadosCita",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EstadosHistoria_Nombre",
                table: "EstadosHistoria",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EstadosPieza_Nombre",
                table: "EstadosPiezaOdontograma",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_EvalPeriodontal_Historia",
                table: "EvaluacionesPeriodontales",
                column: "HistoriaClinicaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HistoriaDiagnosticos_DiagnosticoId",
                table: "HistoriaDiagnosticos",
                column: "DiagnosticoId");

            migrationBuilder.CreateIndex(
                name: "UQ_HistoriaDiag",
                table: "HistoriaDiagnosticos",
                columns: new[] { "HistoriaClinicaId", "DiagnosticoId", "Tipo" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Historias_PacienteId",
                table: "HistoriasClinicas",
                columns: new[] { "PacienteId", "FechaHoraAtencion" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_AnuladoPorUsuarioId",
                table: "HistoriasClinicas",
                column: "AnuladoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_AtencionCorrigeId",
                table: "HistoriasClinicas",
                column: "AtencionCorrigeId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_EstadoId",
                table: "HistoriasClinicas",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoriasClinicas_ProfesionalId",
                table: "HistoriasClinicas",
                column: "ProfesionalId");

            migrationBuilder.CreateIndex(
                name: "UQ_Historias_NumeroHistoria",
                table: "HistoriasClinicas",
                column: "NumeroHistoria",
                unique: true,
                filter: "(\"NumeroHistoria\" IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuenta_HistoriaClinicaId",
                table: "MovimientosCuenta",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuenta_Paciente_Fecha",
                table: "MovimientosCuenta",
                columns: new[] { "PacienteId", "Fecha", "Id" });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuenta_RegistradoPorUsuarioId",
                table: "MovimientosCuenta",
                column: "RegistradoPorUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuenta_TipoMovimientoId",
                table: "MovimientosCuenta",
                column: "TipoMovimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosCuenta_TratamientoId",
                table: "MovimientosCuenta",
                column: "TratamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Odontograma_HistoriaClinicaId",
                table: "OdontogramaDetalle",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Odontograma_Pieza",
                table: "OdontogramaDetalle",
                columns: new[] { "PiezaDentalId", "FechaRegistro" },
                descending: new[] { false, true });

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaDetalle_CaraDentalId",
                table: "OdontogramaDetalle",
                column: "CaraDentalId");

            migrationBuilder.CreateIndex(
                name: "IX_OdontogramaDetalle_EstadoId",
                table: "OdontogramaDetalle",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "UQ_Odontograma_Historia_Pieza_Cara",
                table: "OdontogramaDetalle",
                columns: new[] { "HistoriaClinicaId", "PiezaDentalId", "CaraDentalId" },
                unique: true,
                filter: "(\"CaraDentalId\" IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ_Odontograma_Historia_Pieza_SinCara",
                table: "OdontogramaDetalle",
                columns: new[] { "HistoriaClinicaId", "PiezaDentalId" },
                unique: true,
                filter: "(\"CaraDentalId\" IS NULL)");

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_Apellidos",
                table: "Pacientes",
                columns: new[] { "ApellidoPaterno", "ApellidoMaterno", "Nombres" });

            migrationBuilder.CreateIndex(
                name: "IX_Pacientes_SexoId",
                table: "Pacientes",
                column: "SexoId");

            migrationBuilder.CreateIndex(
                name: "UQ_Pacientes_DNI",
                table: "Pacientes",
                column: "DNI",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Permisos_Modulo_Accion",
                table: "Permisos",
                columns: new[] { "Modulo", "Accion" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Piezas_Codigo",
                table: "PiezasDentales",
                column: "Codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_Roles_Nombre",
                table: "Roles",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermisos_PermisoId",
                table: "RolPermisos",
                column: "PermisoId");

            migrationBuilder.CreateIndex(
                name: "UQ_Sexos_Nombre",
                table: "Sexos",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TiposMovimiento_Nombre",
                table: "TiposMovimientoCuenta",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_TiposTratamiento_Nombre",
                table: "TiposTratamiento",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TratamientoCaras_CaraDentalId",
                table: "TratamientoCaras",
                column: "CaraDentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_HistoriaClinicaId",
                table: "Tratamientos",
                column: "HistoriaClinicaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_PiezaDentalId",
                table: "Tratamientos",
                column: "PiezaDentalId");

            migrationBuilder.CreateIndex(
                name: "IX_Tratamientos_TipoTratamientoId",
                table: "Tratamientos",
                column: "TipoTratamientoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_RolId",
                table: "Usuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "UQ_Usuarios_Email",
                table: "Usuarios",
                column: "Email",
                unique: true,
                filter: "(\"Email\" IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UQ_Usuarios_NombreUsuario",
                table: "Usuarios",
                column: "NombreUsuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Auditoria");

            migrationBuilder.DropTable(
                name: "Citas");

            migrationBuilder.DropTable(
                name: "ConfiguracionSistema");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "EvaluacionesPeriodontales");

            migrationBuilder.DropTable(
                name: "HistoriaDiagnosticos");

            migrationBuilder.DropTable(
                name: "MovimientosCuenta");

            migrationBuilder.DropTable(
                name: "OdontogramaDetalle");

            migrationBuilder.DropTable(
                name: "RolPermisos");

            migrationBuilder.DropTable(
                name: "TratamientoCaras");

            migrationBuilder.DropTable(
                name: "EstadosCita");

            migrationBuilder.DropTable(
                name: "Diagnosticos");

            migrationBuilder.DropTable(
                name: "TiposMovimientoCuenta");

            migrationBuilder.DropTable(
                name: "EstadosPiezaOdontograma");

            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "CarasDentales");

            migrationBuilder.DropTable(
                name: "Tratamientos");

            migrationBuilder.DropTable(
                name: "HistoriasClinicas");

            migrationBuilder.DropTable(
                name: "PiezasDentales");

            migrationBuilder.DropTable(
                name: "TiposTratamiento");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "EstadosHistoria");

            migrationBuilder.DropTable(
                name: "Pacientes");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Sexos");
        }
    }
}
