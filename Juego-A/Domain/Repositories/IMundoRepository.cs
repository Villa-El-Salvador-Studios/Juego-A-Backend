using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IMundoRepository
{
    Task<IEnumerable<Mundo>> ListAsync();
    Task AddAsync(Mundo mundo);
    void Update(Mundo mundo);
    void Remove(Mundo mundo);
    Task<Mundo> FindByNombreAsync(string mundoNombre);
    Task<Mundo> FindByIdAsync(int id);
}