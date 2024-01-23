using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
using JuegoA_API.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace JuegoA_API.Juego_A.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class ObjetosController : ControllerBase
{
    private readonly IObjetoService _objetoService;
    private readonly IMapper _mapper;
    
    public ObjetosController(IObjetoService objetoService, IMapper mapper)
    {
        _objetoService = objetoService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ObjetoResource>> GetAllAsync()
    {
        var objetos = await _objetoService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Objeto>, 
            IEnumerable<ObjetoResource>>(objetos);
        return resources;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var objeto = await _objetoService.ReturnById(id);

        if (objeto == null)
        {
            return NotFound($"Objeto con ID {id} no encontrado.");
        }

        var objetoResource = _mapper.Map<Objeto, ObjetoResource>(objeto);

        return Ok(objetoResource);
    }
}