using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadosPiezaOdontogramaController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public EstadosPiezaOdontogramaController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoPiezaDto>>> Get()
    {
        var items = await _context.EstadosPiezaOdontogramas.AsNoTracking().Select(e => new EstadoPiezaDto { Id = e.Id, Nombre = e.Nombre }).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EstadoPiezaDto>> Get(byte id)
    {
        var item = await _context.EstadosPiezaOdontogramas.AsNoTracking().Where(e => e.Id == id).Select(e => new EstadoPiezaDto { Id = e.Id, Nombre = e.Nombre }).FirstOrDefaultAsync();
        if (item == null) return NotFound(new { mensaje = "Estado no encontrado." });
        return Ok(item);
    }
}
