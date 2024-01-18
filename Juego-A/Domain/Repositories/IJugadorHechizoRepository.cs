using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IJugadorHechizoRepository
{
    Task<IEnumerable<JugadorHechizo>> FindByJugadorIdAsync(int id);
    Task<JugadorHechizo> FindByJugadorIdYHechizoIdAsync(int? jugadorHechizoJugadorId, int? jugadorHechizoHechizoId);
    Task AddAsync(JugadorHechizo jugadorHechizo);
}