using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IPersonajeRepository
{
    Task<IEnumerable<Personaje>> ListAsync();
    Task<Personaje> FindByIdAsync(int id);
    Task<Personaje> FindByNombreAsync(string nombre);
    Task AddAsync(Personaje personaje);
    void Update(Personaje personaje);
    void Remove(Personaje personaje);
}