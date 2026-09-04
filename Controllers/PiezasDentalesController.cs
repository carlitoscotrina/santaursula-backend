using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PiezasDentalesController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public PiezasDentalesController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PiezaDentalDto>>> Get()
    {
        var items = await _context.PiezasDentales.AsNoTracking().Select(p => new PiezaDentalDto { Id = p.Id, Codigo = p.Codigo, Tipo = p.Tipo }).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PiezaDentalDto>> Get(int id)
    {
        var item = await _context.PiezasDentales.AsNoTracking().Where(p => p.Id == id).Select(p => new PiezaDentalDto { Id = p.Id, Codigo = p.Codigo, Tipo = p.Tipo }).FirstOrDefaultAsync();
        if (item == null) return NotFound(new { mensaje = "Pieza dental no encontrada." });
        return Ok(item);
    }
}
