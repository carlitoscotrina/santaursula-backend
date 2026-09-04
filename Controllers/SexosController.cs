using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SexosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;

    public SexosController(SantaUrsulaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SexoDto>>> Get()
    {
        var items = await _context.Sexos
            .AsNoTracking()
            .Select(s => new SexoDto { Id = s.Id, Nombre = s.Nombre })
            .ToListAsync();

        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SexoDto>> Get(byte id)
    {
        var item = await _context.Sexos
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new SexoDto { Id = s.Id, Nombre = s.Nombre })
            .FirstOrDefaultAsync();

        if (item == null) return NotFound(new { mensaje = "Sexo no encontrado." });

        return Ok(item);
    }
}
