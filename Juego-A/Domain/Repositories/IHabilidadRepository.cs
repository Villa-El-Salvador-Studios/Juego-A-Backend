using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IHabilidadRepository
{
    Task<IEnumerable<Habilidad>> ListAsync();
    Task<Habilidad> FindByIdAsync(int id);
}