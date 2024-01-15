using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class MundosController : ControllerBase
{
    private readonly IMundoService _mundoService;
    private readonly IMapper _mapper;
    
    public MundosController(IMundoService mundoService, IMapper mapper)
    {
        _mundoService = mundoService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<MundoResource>> GetAllAsync()
    {
        var mundos = await _mundoService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Mundo>, 
            IEnumerable<MundoResource>>(mundos);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var mundo = await _mundoService.ReturnById(id);
        
        if (mundo == null)
        {
            return NotFound($"Mundo con ID {id} no encontrado.");
        }
        
        var mundoResource = _mapper.Map<Mundo, MundoResource>(mundo);

        return Ok(mundoResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMundoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
 
        var mundo = _mapper.Map<SaveMundoResource, Mundo>(resource);
        var result = await _mundoService.UpdateAsync(id, mundo);
 
        if (!result.Success)
            return BadRequest(result.Message);
        
        var mundoResource = _mapper.Map<Mundo, MundoResource>(result.Resource);
        
        return Ok(mundoResource);
    }
}