using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class ObjetoService : IObjetoService
{
    private readonly IObjetoRepository _objetoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ObjetoService(IObjetoRepository objetoRepository, IUnitOfWork unitOfWork)
    {
        _objetoRepository = objetoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Objeto>> ListAsync()
    {
        return await _objetoRepository.ListAsync();
    }

    public async Task<IEnumerable<Objeto>> ReturnByJugadorId(int id)
    {
        return await _objetoRepository.GetByJugadorId(id);
    }

    public async Task<ObjetoResponse> UpdateAsync(int id, Objeto objeto)
    {
        var existingObjeto = await _objetoRepository.FindIndividualObjectByIdAsync(id);
        
        if (existingObjeto == null)
            return new ObjetoResponse("Jugador no encontrado.");

        existingObjeto.Cantidad = objeto.Cantidad;
        existingObjeto.jugadorId = objeto.jugadorId;
        
        try
        {
            _objetoRepository.Update(existingObjeto);
            await _unitOfWork.CompleteAsync();
            return new ObjetoResponse(existingObjeto);
        }
        catch (Exception e)
        {
            return new ObjetoResponse($"Ocurrio un error al actualizar el objeto: {e.Message}");
        }
    }
}