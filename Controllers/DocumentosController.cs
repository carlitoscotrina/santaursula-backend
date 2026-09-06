using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DocumentosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public DocumentosController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DocumentoDto>>> Get()
    {
        var items = await _context.Documentos.AsNoTracking().Select(d => new DocumentoDto
        {
            Id = d.Id,
            HistoriaClinicaId = d.HistoriaClinicaId,
            TipoDocumento = d.TipoDocumento,
            RutaArchivo = d.RutaArchivo,
            Version = d.Version,
            FechaGeneracion = d.FechaGeneracion,
            GeneradoPorUsuarioId = d.GeneradoPorUsuarioId,
            GeneradoPorUsuarioNombre = d.GeneradoPorUsuario.NombreCompleto
        }).ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult<DocumentoDto>> Crear([FromBody] CrearDocumentoDto dto)
    {
        var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId);
        if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });

        var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Id == dto.GeneradoPorUsuarioId);
        if (!usuarioExiste) return BadRequest(new { mensaje = "Usuario generador no encontrado." });

        var ent = new Data.Models.Documento
        {
            HistoriaClinicaId = dto.HistoriaClinicaId,
            TipoDocumento = dto.TipoDocumento,
            RutaArchivo = dto.RutaArchivo,
            Version = dto.Version,
            FechaGeneracion = DateTime.UtcNow,
            GeneradoPorUsuarioId = dto.GeneradoPorUsuarioId
        };

        _context.Documentos.Add(ent);
        await _context.SaveChangesAsync();

        var created = new DocumentoDto
        {
            Id = ent.Id,
            HistoriaClinicaId = ent.HistoriaClinicaId,
            TipoDocumento = ent.TipoDocumento,
            RutaArchivo = ent.RutaArchivo,
            Version = ent.Version,
            FechaGeneracion = ent.FechaGeneracion,
            GeneradoPorUsuarioId = ent.GeneradoPorUsuarioId,
            GeneradoPorUsuarioNombre = (await _context.Usuarios.FindAsync(ent.GeneradoPorUsuarioId))?.NombreCompleto ?? string.Empty
        };

        return CreatedAtAction(nameof(Get), new { id = ent.Id }, created);
    }
}
