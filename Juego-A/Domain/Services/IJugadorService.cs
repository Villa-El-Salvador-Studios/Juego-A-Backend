using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IJugadorService
{
    Task<IEnumerable<Jugador>> ListAsync();
    Task<Jugador> ReturnById(int id);
    Task<Jugador> GetByUsuarioYContraseniaAsync(string usuario, string contrasenia);
    Task<JugadorResponse> SaveAsync(Jugador jugador);
    Task<JugadorResponse> UpdateAsync(int id, Jugador jugador);
    Task<JugadorResponse> DeleteAsync(int id);
}