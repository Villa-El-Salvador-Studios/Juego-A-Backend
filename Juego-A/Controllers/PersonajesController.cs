using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PersonajesController : ControllerBase
{
    private readonly IPersonajeService _personajeService;
    private readonly IMapper _mapper;

    public PersonajesController(IPersonajeService personajeService, IMapper mapper)
    {
        _personajeService = personajeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<PersonajeResource>> GetAllAsync()
    {
        var personajes = await _personajeService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Personaje>, IEnumerable<PersonajeResource>>(personajes);
        return resources;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var personaje = await _personajeService.ReturnById(id);
        
        if (personaje == null)
        {
            return NotFound($"Personaje con ID {id} no encontrado.");
        }
        
        var personajeResource = _mapper.Map<Personaje, PersonajeResource>(personaje);

        return Ok(personajeResource);
    }

    [HttpGet("jugador/{jugadorId}")]
    public async Task<IActionResult> GetByJugadorId(int jugadorId)
    {
        var personajes = await _personajeService.ReturnByJugadorId(jugadorId);
        
        if (personajes == null)
        {
            return NotFound($"No existen personajes ligados al jugador con el ID: {jugadorId}.");
        }
        
        var personajesResource = _mapper.Map<IEnumerable<Personaje>, IEnumerable<PersonajeResource>>(personajes);

        return Ok(personajesResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SavePersonajeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var personaje = _mapper.Map<SavePersonajeResource, Personaje>(resource);
        var result = await _personajeService.SaveAsync(personaje);

        if (!result.Success)
            return BadRequest(result.Message);

        var personajeResource = _mapper.Map<Personaje, PersonajeResource>(result.Resource);

        return Ok(personajeResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SavePersonajeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var personaje = _mapper.Map<SavePersonajeResource, Personaje>(resource);
        var result = await _personajeService.UpdateAsync(id, personaje);

        if (!result.Success)
            return BadRequest(result.Message);

        var personajeResource = _mapper.Map<Personaje, PersonajeResource>(result.Resource);

        return Ok(personajeResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _personajeService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);

        var personajeResource = _mapper.Map<Personaje, PersonajeResource>(result.Resource);

        return Ok(personajeResource);
    }
}