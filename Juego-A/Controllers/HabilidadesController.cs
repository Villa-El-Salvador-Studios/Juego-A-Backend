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
    private readonly IHabilidadService _habilidadService;
    private readonly IMapper _mapper;
    
    public HabilidadesController(IHabilidadService habilidadService, IMapper mapper)
    {
        _habilidadService = habilidadService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<HabilidadResource>> GetAllAsync()
    {
        var habilidades = await _habilidadService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Habilidad>, 
            IEnumerable<HabilidadResource>>(habilidades);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var habilidades = await _habilidadService.ReturnById(id);
        
        if (habilidades == null)
        {
            return NotFound($"Set de habilidades con ID {id} no encontrado.");
        }
        
        var habilidadesResource = _mapper.Map<Habilidad, HabilidadResource>(habilidades);

        return Ok(habilidadesResource);
    }
}