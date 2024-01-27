using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class PersonajeService : IPersonajeService
{
    private readonly IPersonajeRepository _personajeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PersonajeService(IPersonajeRepository personajeRepository, IUnitOfWork unitOfWork)
    {
        _personajeRepository = personajeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Personaje>> ListAsync()
    {
        return await _personajeRepository.ListAsync();
    }

    public async Task<IEnumerable<Personaje>> ReturnByJugadorId(int jugadorId)
    {
        return await _personajeRepository.FindByJugadorIdAsync(jugadorId);
    }

    public async Task<Personaje> ReturnById(int id)
    {
        return await _personajeRepository.FindByIdAsync(id);
    }

    public async Task<PersonajeResponse> SaveAsync(Personaje personaje)
    {
        var existingPersonaje = await _personajeRepository.FindByNombreAndJugadorIdAsync(personaje.Nombre, personaje.JugadorId);
        
        if (existingPersonaje != null)
            return new PersonajeResponse("Ya existe un personaje con ese nombre registrado.");
        
        try
        {
            await _personajeRepository.AddAsync(personaje);
            await _unitOfWork.CompleteAsync();
            return new PersonajeResponse(personaje);
        }
        catch (Exception e)
        {
            return new PersonajeResponse($"Ocurrio un error al registrar al personaje: {e.Message}");
        }
    }

    public async Task<PersonajeResponse> UpdateAsync(int id, Personaje personaje)
    {
        var existingPersonaje = await _personajeRepository.FindByIdAsync(id);
        
        if (existingPersonaje == null)
            return new PersonajeResponse("Personaje no encontrado.");
        
        existingPersonaje.Vida = personaje.Vida;
        existingPersonaje.Nivel = personaje.Nivel;
        existingPersonaje.Nombre = personaje.Nombre;
        existingPersonaje.Ataque = personaje.Ataque;
        existingPersonaje.Experiencia = personaje.Experiencia;
        existingPersonaje.Imagen = personaje.Imagen;
        
        try
        {
            _personajeRepository.Update(existingPersonaje);
            await _unitOfWork.CompleteAsync();
            return new PersonajeResponse(existingPersonaje);
        }
        catch (Exception e)
        {
            return new PersonajeResponse($"Ocurrio un error al actualizar al personaje: {e.Message}");
        }
    }

    public async Task<PersonajeResponse> DeleteAsync(int id)
    {
        var existingPersonaje = await _personajeRepository.FindByIdAsync(id);
        if (existingPersonaje == null)
            return new PersonajeResponse("Personaje no encontrado.");
        try
        {
            _personajeRepository.Remove(existingPersonaje);
            await _unitOfWork.CompleteAsync();
            return new PersonajeResponse(existingPersonaje);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new PersonajeResponse($"Ocurrio un error al intentar eliminar al personaje: {e.Message}");
        }
    }
}