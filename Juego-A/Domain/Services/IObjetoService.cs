using System.Collections;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IObjetoService
{
    Task<IEnumerable<Objeto>> ListAsync();
    Task<Objeto> ReturnById(int id);
}