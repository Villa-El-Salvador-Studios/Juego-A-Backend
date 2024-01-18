using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class JugadorHechizoService : IJugadorHechizoService
{
    private readonly IJugadorHechizoRepository _jugadorHechizoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JugadorHechizoService(IJugadorHechizoRepository jugadorHechizoRepository, IUnitOfWork unitOfWork)
    {
        _jugadorHechizoRepository = jugadorHechizoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<JugadorHechizo>> ReturnByJugadorId(int id)
    {
        return await _jugadorHechizoRepository.FindByJugadorIdAsync(id);
    }

    public async Task<JugadorHechizoResponse> SaveAsync(JugadorHechizo jugadorHechizo)
    {
        var existingJugadorHechizo = await _jugadorHechizoRepository.FindByJugadorIdYHechizoIdAsync(jugadorHechizo.JugadorId, jugadorHechizo.HechizoId);
        
        if (existingJugadorHechizo != null)
            return new JugadorHechizoResponse("Ya existe un registro de un jugador con ese hechizo.");
        
        try
        {
            await _jugadorHechizoRepository.AddAsync(jugadorHechizo);
            await _unitOfWork.CompleteAsync();
            return new JugadorHechizoResponse(jugadorHechizo);
        }
        catch (Exception e)
        {
            return new JugadorHechizoResponse($"Ocurrio un error al registrar la relación entre el jugador y el hechizo. Error: {e.Message}");
        }
    }
}