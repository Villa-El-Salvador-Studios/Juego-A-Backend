using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class HechizosController : ControllerBase
{
    private readonly IHechizoService _hechizoService;
    private readonly IMapper _mapper;

    public HechizosController(IHechizoService hechizoService, IMapper mapper)
    {
        _hechizoService = hechizoService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<HechizoResource>> GetAllAsync()
    {
        var hechizos = await _hechizoService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Hechizo>, 
            IEnumerable<HechizoResource>>(hechizos);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var hechizo = await _hechizoService.ReturnById(id);
        
        if (hechizo == null)
        {
            return NotFound($"Hechizo con ID {id} no encontrado.");
        }
        
        var hechizoResource = _mapper.Map<Hechizo, HechizoResource>(hechizo);

        return Ok(hechizoResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveHechizoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
 
        var hechizo = _mapper.Map<SaveHechizoResource, Hechizo>(resource);
        var result = await _hechizoService.UpdateAsync(id, hechizo);
 
        if (!result.Success)
            return BadRequest(result.Message);
        
        var hechizoResource = _mapper.Map<Hechizo, HechizoResource>(result.Resource);
        
        return Ok(hechizoResource);
    }
}