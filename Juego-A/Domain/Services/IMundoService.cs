using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IMundoService
{
    Task<IEnumerable<Mundo>> ListAsync();
    Task<MundoResponse> SaveAsync(Mundo mundo);
    Task<MundoResponse> UpdateAsync(int id, Mundo mundo);
    Task<MundoResponse> DeleteAsync(int id);
    Task<Mundo> ReturnById(int id);
}