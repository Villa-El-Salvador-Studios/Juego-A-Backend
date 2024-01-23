using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IObjetoRepository
{
    Task<IEnumerable<Objeto>> ListAsync();
    Task<Objeto> FindByIdAsync(int id);
}