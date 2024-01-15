using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IHabilidadesRepository
{
    Task<IEnumerable<Habilidades>> ListAsync();
    Task<Habilidades> FindByIdAsync(int id);
    Task AddAsync(Habilidades habilidades);
    void Update(Habilidades habilidades);
    void Remove(Habilidades habilidades);
}