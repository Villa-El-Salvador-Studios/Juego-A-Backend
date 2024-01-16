using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IObjetoRepository
{
    Task<IEnumerable<Objeto>> ListAsync();
    Task AddAsync(Objeto objeto);
    void Remove(Objeto objeto);
    void Update(Objeto objeto);
    Task<IEnumerable<Objeto>> GetByJugadorId(int id);
    Task<Objeto> FindIndividualObjectByIdAsync(int id);
}