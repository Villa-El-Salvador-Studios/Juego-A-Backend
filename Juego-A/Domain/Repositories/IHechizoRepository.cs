using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IHechizoRepository
{
    Task<IEnumerable<Hechizo>> ListAsync();
    Task<Hechizo> FindByIdAsync(int id);
    void Update(Hechizo hechizo);
}