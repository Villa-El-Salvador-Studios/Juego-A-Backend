using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IJugadorHechizoService
{
    Task<IEnumerable<JugadorHechizo>> ReturnByJugadorId(int id);
    Task<JugadorHechizoResponse> SaveAsync(JugadorHechizo jugadorHechizo);
}