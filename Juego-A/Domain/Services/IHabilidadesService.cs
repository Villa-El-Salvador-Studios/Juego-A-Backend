using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IHabilidadesService
{
    Task<IEnumerable<Habilidades>> ListAsync();
    Task<Habilidades> ReturnById(int id);
}