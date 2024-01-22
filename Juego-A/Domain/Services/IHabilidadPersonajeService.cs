using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IHabilidadPersonajeService
{
    Task<IEnumerable<HabilidadPersonaje>> ReturnByPersonajeId(int personajeId);
    Task<HabilidadPersonajeResponse> SaveAsync(HabilidadPersonaje habilidadPersonaje);
}