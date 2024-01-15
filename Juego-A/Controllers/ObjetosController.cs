using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Resources;
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
        var objetos = await _objetoService.ReturnById(id);

        if (objetos == null || !objetos.Any())
        {
            return NotFound($"Objetos con jugador ID {id} no encontrados.");
        }

        var objetoResources = _mapper.Map<IEnumerable<Objeto>, IEnumerable<ObjetoResource>>(objetos);

        return Ok(objetoResources);
    }
}