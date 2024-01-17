using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IHechizoService
{
    Task<IEnumerable<Hechizo>> ListAsync();
    Task<Hechizo> ReturnById(int id);
    Task<HechizoResponse> UpdateAsync(int id, Hechizo hechizo);
}