using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IJugadorObjetoRepository
{
    Task<JugadorObjeto> FindByJugadorIdAndObjetoId(int? jugadorId, int? objetoId);
    Task AddAsync(JugadorObjeto jugadorObjeto);
    void Update(JugadorObjeto jugadorObjeto);
}