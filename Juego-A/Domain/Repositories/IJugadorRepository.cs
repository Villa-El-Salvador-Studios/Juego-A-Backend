using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IJugadorRepository
{
    Task<IEnumerable<Jugador>> ListAsync();
    Task AddAsync(Jugador jugador);
    void Update(Jugador jugador);
    void Remove(Jugador jugador);
}