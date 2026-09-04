using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermisosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public PermisosController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PermisoDto>>> Get()
    {
        var items = await _context.Permisos.AsNoTracking().Select(p => new PermisoDto { Id = p.Id, Modulo = p.Modulo, Accion = p.Accion }).ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PermisoDto>> Get(int id)
    {
        var item = await _context.Permisos.AsNoTracking().Where(p => p.Id == id).Select(p => new PermisoDto { Id = p.Id, Modulo = p.Modulo, Accion = p.Accion }).FirstOrDefaultAsync();
        if (item == null) return NotFound(new { mensaje = "Permiso no encontrado." });
        return Ok(item);
    }
}
