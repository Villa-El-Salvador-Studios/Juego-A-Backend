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
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveMundoResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        
        var mundo = _mapper.Map<SaveMundoResource, Mundo>(resource);
        var result = await _mundoService.SaveAsync(mundo);
        
        if (!result.Success)
            return BadRequest(result.Message);
        
        var mundoResource = _mapper.Map<Mundo, MundoResource>(result.Resource);
        
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
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _mundoService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);
 
        var mundoResource = _mapper.Map<Mundo, MundoResource>(result.Resource);
        
        return Ok(mundoResource);
    }
}