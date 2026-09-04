using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadosCitasController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public EstadosCitasController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoCitaDto>>> Get()
    {
        var items = await _context.EstadosCita.AsNoTracking().Select(e => new EstadoCitaDto { Id = e.Id, Nombre = e.Nombre }).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoCitaDto>> Get(byte id)
    {
        var item = await _context.EstadosCita.AsNoTracking().Where(e => e.Id == id).Select(e => new EstadoCitaDto { Id = e.Id, Nombre = e.Nombre }).FirstOrDefaultAsync();
        if (item == null) return NotFound(new { mensaje = "Estado no encontrado." });
        return Ok(item);
    }
}
