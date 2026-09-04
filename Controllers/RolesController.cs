using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RolesController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public RolesController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoleDto>>> Get()
    {
        var items = await _context.Roles.AsNoTracking().Select(r => new RoleDto { Id = r.Id, Nombre = r.Nombre }).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RoleDto>> Get(int id)
    {
        var item = await _context.Roles.AsNoTracking().Where(r => r.Id == id).Select(r => new RoleDto { Id = r.Id, Nombre = r.Nombre }).FirstOrDefaultAsync();
        if (item == null) return NotFound(new { mensaje = "Rol no encontrado." });
        return Ok(item);
    }
}
