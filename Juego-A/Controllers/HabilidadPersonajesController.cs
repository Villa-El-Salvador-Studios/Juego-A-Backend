using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HabilidadPersonajesController : ControllerBase
{
    private readonly IHabilidadPersonajeService _habilidadPersonajeService;
    private readonly IMapper _mapper;

    public HabilidadPersonajesController(IHabilidadPersonajeService habilidadPersonajeService, IMapper mapper)
    {
        _habilidadPersonajeService = habilidadPersonajeService;
        _mapper = mapper;
    }

    [HttpGet("{personajeId}")]
    public async Task<IActionResult> GetAllHabilidadesByPersonajeId(int personajeId)
    {
        var habilidades = await _habilidadPersonajeService.ReturnByPersonajeId(personajeId);

        if (habilidades == null)
        {
            return NotFound($"No hay habilidades ligadas al personaje con el ID {personajeId}.");
        }

        var habilidadpersonajesResource = _mapper.Map<IEnumerable<HabilidadPersonaje>, IEnumerable<HabilidadPersonajeResource>>(habilidades);

        return Ok(habilidadpersonajesResource);
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] HabilidadPersonajeResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var habilidadPersonaje = _mapper.Map<HabilidadPersonajeResource, HabilidadPersonaje>(resource);
        var result = await _habilidadPersonajeService.SaveAsync(habilidadPersonaje);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var habilidadPersonajesResource = _mapper.Map<HabilidadPersonaje, HabilidadPersonajeResource>(result.Resource);
        
        return Ok(habilidadPersonajesResource);
    }
}