using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarasDentalesController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public CarasDentalesController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CaraDentalDto>>> Get()
    {
        var items = await _context.CarasDentales.AsNoTracking().Select(c => new CaraDentalDto { Id = c.Id, Nombre = c.Nombre }).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CaraDentalDto>> Get(int id)
    {
        var item = await _context.CarasDentales.AsNoTracking().Where(c => c.Id == id).Select(c => new CaraDentalDto { Id = c.Id, Nombre = c.Nombre }).FirstOrDefaultAsync();
        if (item == null) return NotFound(new { mensaje = "Cara dental no encontrada." });
        return Ok(item);
    }
}
