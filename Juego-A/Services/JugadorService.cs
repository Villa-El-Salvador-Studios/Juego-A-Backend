using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class JugadorService : IJugadorService
{
    private readonly IJugadorRepository _jugadorRepository;
    private readonly IUnitOfWork _unitOfWork;

    public JugadorService(IJugadorRepository jugadorRepository, IUnitOfWork unitOfWork)
    {
        _jugadorRepository = jugadorRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Jugador>> ListAsync()
    {
        return await _jugadorRepository.ListAsync();
    }

    public async Task<JugadorResponse> SaveAsync(Jugador jugador)
    {
        var existingUsuario = await _jugadorRepository.FindByUsuarioAsync(jugador.Usuario);
        
        if (existingUsuario != null)
            return new JugadorResponse("Ya existe un jugador con ese nombre de usuario registrado.");
        
        try
        {
            await _jugadorRepository.AddAsync(jugador);
            await _unitOfWork.CompleteAsync();
            return new JugadorResponse(jugador);
        }
        catch (Exception e)
        {
            return new JugadorResponse($"Ocurrio un error al registrar al jugador: {e.Message}");
        }
    }

    public async Task<JugadorResponse> UpdateAsync(int id, Jugador jugador)
    {
        var existingJugador = await _jugadorRepository.FindByIdAsync(id);
        
        if (existingJugador == null)
            return new JugadorResponse("Jugador no encontrado.");
        
        existingJugador.Contrasenia = jugador.Contrasenia;
        existingJugador.fotoPerfil = jugador.fotoPerfil;
        
        try
        {
            _jugadorRepository.Update(existingJugador);
            await _unitOfWork.CompleteAsync();
            return new JugadorResponse(existingJugador);
        }
        catch (Exception e)
        {
            return new JugadorResponse($"Ocurrio un error al actualizar al jugador: {e.Message}");
        }
    }

    public async Task<JugadorResponse> DeleteAsync(int id)
    {
        var existingJugador = await _jugadorRepository.FindByIdAsync(id);
        if (existingJugador == null)
            return new JugadorResponse("Jugador no encontrado.");
        try
        {
            _jugadorRepository.Remove(existingJugador);
            await _unitOfWork.CompleteAsync();
            return new JugadorResponse(existingJugador);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new JugadorResponse($"Ocurrio un error al intentar eliminar al jugador: {e.Message}");
        }
    }
}