using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services;

public interface IJugadorObjetoService
{
    Task<JugadorObjeto> ReturnByJugadorIdAndObjetoId(int? jugadorId, int? objetoId);
    Task<JugadorObjetoResponse> SaveAsync(JugadorObjeto jugadorObjeto);
    Task<JugadorObjetoResponse> UpdateAsync(int jugadorId, int objetoId, JugadorObjeto jugadorObjeto);
}