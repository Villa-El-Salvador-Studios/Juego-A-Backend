using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JugadorHechizosController : ControllerBase
{
    private readonly IJugadorHechizoService _jugadorHechizoService;
    private readonly IMapper _mapper;

    public JugadorHechizosController(IJugadorHechizoService jugadorHechizoService, IMapper mapper)
    {
        _jugadorHechizoService = jugadorHechizoService;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAllHechizosByJugadorId(int id)
    {
        var hechizos = await _jugadorHechizoService.ReturnByJugadorId(id);

        if (hechizos == null)
        {
            return NotFound($"No hay hechizos ligados al jugador con el ID {id}.");
        }

        var jugadorHechizosResource = _mapper.Map<IEnumerable<JugadorHechizo>, IEnumerable<JugadorHechizoResource>>(hechizos);

        return Ok(jugadorHechizosResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] JugadorHechizoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var jugadorHechizo = _mapper.Map<JugadorHechizoResource, JugadorHechizo>(resource);
        var result = await _jugadorHechizoService.SaveAsync(jugadorHechizo);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var jugadorHechizoResource = _mapper.Map<JugadorHechizo, JugadorHechizoResource>(result.Resource);
        
        return Ok(jugadorHechizoResource);
    }
}