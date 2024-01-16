using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HabilidadesController : ControllerBase
{
    private readonly IHabilidadesService _habilidadesService;
    private readonly IMapper _mapper;
    
    public HabilidadesController(IHabilidadesService habilidadesService, IMapper mapper)
    {
        _habilidadesService = habilidadesService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<HabilidadesResource>> GetAllAsync()
    {
        var habilidades = await _habilidadesService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Habilidades>, 
            IEnumerable<HabilidadesResource>>(habilidades);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var habilidades = await _habilidadesService.ReturnById(id);
        
        if (habilidades == null)
        {
            return NotFound($"Set de habilidades con ID {id} no encontrado.");
        }
        
        var habilidadesResource = _mapper.Map<Habilidades, HabilidadesResource>(habilidades);

        return Ok(habilidadesResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveHabilidadesResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
 
        var habilidades = _mapper.Map<SaveHabilidadesResource, Habilidades>(resource);
        var result = await _habilidadesService.UpdateAsync(id, habilidades);
 
        if (!result.Success)
            return BadRequest(result.Message);
        
        var habilidadesResource = _mapper.Map<Habilidades, HabilidadesResource>(result.Resource);
        
        return Ok(habilidadesResource);
    }
}