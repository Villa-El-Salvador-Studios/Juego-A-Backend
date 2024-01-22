using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class HabilidadPersonajeService : IHabilidadPersonajeService
{
    private readonly IHabilidadPersonajeRepository _habilidadPersonajeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HabilidadPersonajeService(IHabilidadPersonajeRepository habilidadPersonajeRepository, IUnitOfWork unitOfWork)
    {
        _habilidadPersonajeRepository = habilidadPersonajeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<HabilidadPersonaje>> ReturnByPersonajeId(int personajeId)
    {
        return await _habilidadPersonajeRepository.FindByPersonajeIdAsync(personajeId);
    }

    public async Task<HabilidadPersonajeResponse> SaveAsync(HabilidadPersonaje habilidadPersonaje)
    {
        var existingHabilidadPersonaje = await _habilidadPersonajeRepository.FindByHabilidadIdYPersonajeIdAsync(habilidadPersonaje.HabilidadId, habilidadPersonaje.PersonajeId);
        
        if (existingHabilidadPersonaje != null)
            return new HabilidadPersonajeResponse("Ya existe un registro de un personaje con esa habilidad.");
        
        try
        {
            await _habilidadPersonajeRepository.AddAsync(habilidadPersonaje);
            await _unitOfWork.CompleteAsync();
            return new HabilidadPersonajeResponse(habilidadPersonaje);
        }
        catch (Exception e)
        {
            return new HabilidadPersonajeResponse($"Ocurrio un error al registrar la relación entre el personaje y la habilidad. Error: {e.Message}");
        }
    }
}