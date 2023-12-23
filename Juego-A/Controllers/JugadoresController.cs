using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class JugadoresController : ControllerBase
{
    private readonly IJugadorService _jugadorService;
    private readonly IMapper _mapper;

    public JugadoresController(IJugadorService jugadorService, IMapper mapper)
    {
        _jugadorService = jugadorService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<JugadorResource>> GetAllAsync()
    {
        var jugadores = await _jugadorService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Jugador>, 
            IEnumerable<JugadorResource>>(jugadores);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var jugador = await _jugadorService.ReturnById(id);
        
        if (jugador == null)
        {
            return NotFound($"Jugador con ID {id} no encontrado.");
        }
        
        var jugadorResource = _mapper.Map<Jugador, JugadorResource>(jugador);

        return Ok(jugadorResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveJugadorResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var jugador = _mapper.Map<SaveJugadorResource, Jugador>(resource);
        var result = await _jugadorService.SaveAsync(jugador);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var jugadorResource = _mapper.Map<Jugador, JugadorResource>(result.Resource);
        
        return Ok(jugadorResource);
    }
    
    [HttpPost("buscar")]
    public async Task<IActionResult> BuscarJugadorAsync([FromBody] LoginResource loginResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var jugador = await _jugadorService.GetByUsuarioYContraseniaAsync(loginResource.Usuario, loginResource.Contraseña);

        if (jugador == null)
        {
            return NotFound("Jugador no encontrado con las credenciales proporcionadas.");
        }

        var jugadorResource = _mapper.Map<Jugador, JugadorResource>(jugador);

        return Ok(jugadorResource);
    }

    [HttpPost("verificar-usuario")]
    public async Task<IActionResult> BuscarUsuario([FromBody] VerifyUserResource verifyUserResource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var jugador = await _jugadorService.GetByUsuario(verifyUserResource.Usuario);

        if (jugador == null)
        {
            return NotFound("No existe ese usuario en la base de datos.");
        }

        var jugadorResource = _mapper.Map<Jugador, JugadorResource>(jugador);

        return Ok(jugadorResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveJugadorResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
 
        var jugador = _mapper.Map<SaveJugadorResource, Jugador>(resource);
        var result = await _jugadorService.UpdateAsync(id, jugador);
 
        if (!result.Success)
            return BadRequest(result.Message);
        
        var jugadorResource = _mapper.Map<Jugador, JugadorResource>(result.Resource);
        
        return Ok(jugadorResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _jugadorService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
 
        var jugadorResource = _mapper.Map<Jugador, JugadorResource>(result.Resource);
        
        return Ok(jugadorResource);
    }
}