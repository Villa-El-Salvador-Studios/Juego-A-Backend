using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class JugadorObjetoService : IJugadorObjetoService
{
    private readonly IJugadorObjetoRepository _jugadorObjetoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JugadorObjetoService(IJugadorObjetoRepository jugadorObjetoRepository, IUnitOfWork unitOfWork)
    {
        _jugadorObjetoRepository = jugadorObjetoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<JugadorObjeto> ReturnByJugadorIdAndObjetoId(int? jugadorId, int? objetoId)
    {
        return await _jugadorObjetoRepository.FindByJugadorIdAndObjetoId(jugadorId, objetoId);
    }

    public async Task<JugadorObjetoResponse> SaveAsync(JugadorObjeto jugadorObjeto)
    {
        var existingJugadorObjeto = await _jugadorObjetoRepository.FindByJugadorIdAndObjetoId(jugadorObjeto.JugadorId, jugadorObjeto.ObjetoId);
        
        if (existingJugadorObjeto != null)
            return new JugadorObjetoResponse("Ya existe un registro de un jugador con ese objeto.");
        
        try
        {
            await _jugadorObjetoRepository.AddAsync(jugadorObjeto);
            await _unitOfWork.CompleteAsync();
            return new JugadorObjetoResponse(jugadorObjeto);
        }
        catch (Exception e)
        {
            return new JugadorObjetoResponse($"Ocurrio un error al registrar la relación entre el jugador y el objeto. Error: {e.Message}");
        }
    }

    public async Task<JugadorObjetoResponse> UpdateAsync(int jugadorId, int objetoId, JugadorObjeto jugadorObjeto)
    {
        var existingJugadorObjeto = await _jugadorObjetoRepository.FindByJugadorIdAndObjetoId(jugadorId, objetoId);

        if (existingJugadorObjeto == null)
            return new JugadorObjetoResponse($"El juagdor con el ID {jugadorId} no tiene ningún objeto con el ID {objetoId} en su inventario.");

        existingJugadorObjeto.Cantidad = jugadorObjeto.Cantidad;

        try
        {
            _jugadorObjetoRepository.Update(existingJugadorObjeto);
            await _unitOfWork.CompleteAsync();
            return new JugadorObjetoResponse(existingJugadorObjeto);
        }
        catch (Exception e)
        {
            return new JugadorObjetoResponse(
                $"Ocurrió un error al actualizar la cantidad de objetos con el ID {objetoId} que tiene el jugador con el ID {jugadorId} en su inventario.");
        }
    }
}