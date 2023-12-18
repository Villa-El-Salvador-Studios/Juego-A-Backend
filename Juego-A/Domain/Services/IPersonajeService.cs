using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IPersonajeService
{
    Task<IEnumerable<Personaje>> ListAsync();
    Task<PersonajeResponse> SaveAsync(Personaje personaje);
    Task<PersonajeResponse> UpdateAsync(int id, Personaje personaje);
    Task<PersonajeResponse> DeleteAsync(int id);
}