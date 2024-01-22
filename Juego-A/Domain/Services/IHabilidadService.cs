using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IHabilidadService
{
    Task<IEnumerable<Habilidad>> ListAsync();
    Task<Habilidad> ReturnById(int id);
}