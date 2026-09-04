using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SantaUrsula.API.Data;
using SantaUrsula.API.DTOs;

namespace SantaUrsula.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TiposTratamientosController : ControllerBase
{
    private readonly SantaUrsulaDbContext _context;
    public TiposTratamientosController(SantaUrsulaDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TiposTratamientoDto>>> Get()
    {
        var items = await _context.TiposTratamientos.AsNoTracking().Select(t => new TiposTratamientoDto
        {
            Id = t.Id,
            Nombre = t.Nombre,
            RequierePieza = t.RequierePieza,
            RequiereCara = t.RequiereCara,
            PrecioReferencial = t.PrecioReferencial,
            Activo = t.Activo
        }).ToListAsync();
        return Ok(items);
    }
}
