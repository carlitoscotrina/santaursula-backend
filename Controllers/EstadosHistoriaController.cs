using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstadosHistoriaController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public EstadosHistoriaController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<EstadoHistoriaDto>>> Get()
    {
        var items = await _context.EstadosHistoria.AsNoTracking().Select(e => new EstadoHistoriaDto { Id = e.Id, Nombre = e.Nombre }).ToListAsync();
        return Ok(items);
    }
}
