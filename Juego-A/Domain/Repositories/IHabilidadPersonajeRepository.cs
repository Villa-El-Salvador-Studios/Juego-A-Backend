using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IHabilidadPersonajeRepository
{
    Task<IEnumerable<HabilidadPersonaje>> FindByPersonajeIdAsync(int personajeId);
    Task<HabilidadPersonaje> FindByHabilidadIdYPersonajeIdAsync(int? habilidadId, int? personajeId);
    Task AddAsync(HabilidadPersonaje habilidadPersonaje);
}