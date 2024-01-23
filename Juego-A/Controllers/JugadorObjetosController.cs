using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JugadorObjetosController : ControllerBase
{
    private readonly IJugadorObjetoService _jugadorObjetoService;
    private readonly IMapper _mapper;

    public JugadorObjetosController(IJugadorObjetoService jugadorObjetoService, IMapper mapper)
    {
        _jugadorObjetoService = jugadorObjetoService;
        _mapper = mapper;
    }

    [HttpGet("{jugadorId}/{objetoId}")]
    public async Task<IActionResult> GetAmountByJugadorIdAndObjetoId(int jugadorId, int objetoId)
    {
        var cantidad = await _jugadorObjetoService.ReturnByJugadorIdAndObjetoId(jugadorId, objetoId);

        if (cantidad == null)
        {
            return NotFound($"El jugador con el ID {jugadorId} no tiene ningun objeto con el ID {objetoId} en su inventario.");
        }

        var jugadorObjetosResource = _mapper.Map<JugadorObjeto, JugadorObjetoResource>(cantidad);

        return Ok(jugadorObjetosResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] JugadorObjetoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var jugadorObjeto = _mapper.Map<JugadorObjetoResource, JugadorObjeto>(resource);
        var result = await _jugadorObjetoService.SaveAsync(jugadorObjeto);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var jugadorObjetoResource = _mapper.Map<JugadorObjeto, JugadorObjetoResource>(result.Resource);
        
        return Ok(jugadorObjetoResource);
    }
    
    [HttpPut("{jugadorId}/{objetoId}")]
    public async Task<IActionResult> PutAsync(int jugadorId, int objetoId, [FromBody] JugadorObjetoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
 
        var jugadorObjeto = _mapper.Map<JugadorObjetoResource, JugadorObjeto>(resource);
        var result = await _jugadorObjetoService.UpdateAsync(jugadorId, objetoId, jugadorObjeto);
 
        if (!result.Success)
            return BadRequest(result.Message);
        
        var jugadorObjetoResource = _mapper.Map<JugadorObjeto, JugadorObjetoResource>(result.Resource);
        
        return Ok(jugadorObjetoResource);
    }
}