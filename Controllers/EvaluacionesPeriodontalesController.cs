using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EvaluacionesPeriodontalesController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public EvaluacionesPeriodontalesController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EvaluacionPeriodontalDto>>> Get()
    {
        var items = await _context.EvaluacionesPeriodontales.AsNoTracking().Select(e => new EvaluacionPeriodontalDto
        {
            Id = e.Id,
            HistoriaClinicaId = e.HistoriaClinicaId,
            PbBlanda = e.PbBlanda,
            PbDura = e.PbDura,
            CalculosInfragingivales = e.CalculosInfragingivales,
            Gingivitis = e.Gingivitis,
            BolsaPeriodontal = e.BolsaPeriodontal,
            Especificaciones = e.Especificaciones,
            Pronostico = e.Pronostico
        }).ToListAsync();

        return Ok(items);
    }

    [HttpPost]
    public async Task<ActionResult<EvaluacionPeriodontalDto>> Crear([FromBody] CrearEvaluacionPeriodontalDto dto)
    {
        var hcExiste = await _context.HistoriasClinicas.AnyAsync(h => h.Id == dto.HistoriaClinicaId);
        if (!hcExiste) return BadRequest(new { mensaje = "Historia clínica no encontrada." });

        var ent = new Data.Models.EvaluacionesPeriodontale
        {
            HistoriaClinicaId = dto.HistoriaClinicaId,
            PbBlanda = dto.PbBlanda,
            PbDura = dto.PbDura,
            CalculosInfragingivales = dto.CalculosInfragingivales,
            Gingivitis = dto.Gingivitis,
            BolsaPeriodontal = dto.BolsaPeriodontal,
            Especificaciones = dto.Especificaciones,
            Pronostico = dto.Pronostico
        };

        _context.EvaluacionesPeriodontales.Add(ent);
        await _context.SaveChangesAsync();

        var created = new EvaluacionPeriodontalDto
        {
            Id = ent.Id,
            HistoriaClinicaId = ent.HistoriaClinicaId,
            PbBlanda = ent.PbBlanda,
            PbDura = ent.PbDura,
            CalculosInfragingivales = ent.CalculosInfragingivales,
            Gingivitis = ent.Gingivitis,
            BolsaPeriodontal = ent.BolsaPeriodontal,
            Especificaciones = ent.Especificaciones,
            Pronostico = ent.Pronostico
        };

        return CreatedAtAction(nameof(Get), new { id = ent.Id }, created);
    }
}
