using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class MundoService : IMundoService
{
    private readonly IMundoRepository _mundoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MundoService(IMundoRepository mundoRepository, IUnitOfWork unitOfWork)
    {
        _mundoRepository = mundoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Mundo>> ListAsync()
    {
        return await _mundoRepository.ListAsync();
    }

    public async Task<MundoResponse> SaveAsync(Mundo mundo)
    {
        var existingMundo = await _mundoRepository.FindByNombreAsync(mundo.Nombre);
        
        if (existingMundo != null)
            return new MundoResponse("Ya existe un mundo con ese nombre registrado.");
        
        try
        {
            await _mundoRepository.AddAsync(mundo);
            await _unitOfWork.CompleteAsync();
            return new MundoResponse(mundo);
        }
        catch (Exception e)
        {
            return new MundoResponse($"Ocurrio un error al registrar el mundo: {e.Message}");
        }
    }

    public async Task<MundoResponse> UpdateAsync(int id, Mundo mundo)
    {
        var existingMundo = await _mundoRepository.FindByIdAsync(id);
        
        if (existingMundo == null)
            return new MundoResponse("Mundo no encontrado.");

        existingMundo.Nombre = mundo.Nombre;
        existingMundo.ImagenFondo = mundo.ImagenFondo;
        existingMundo.Estado = mundo.Estado;
        existingMundo.Personaje_Id = mundo.Personaje_Id;
        existingMundo.Xp = mundo.Xp;
        existingMundo.SongId = mundo.SongId;
        
        try
        {
            _mundoRepository.Update(existingMundo);
            await _unitOfWork.CompleteAsync();
            return new MundoResponse(existingMundo);
        }
        catch (Exception e)
        {
            return new MundoResponse($"Ocurrio un error al actualizar el mundo: {e.Message}");
        }
    }

    public async Task<MundoResponse> DeleteAsync(int id)
    {
        var existingMundo = await _mundoRepository.FindByIdAsync(id);
        if (existingMundo == null)
            return new MundoResponse("Mundo no encontrado.");
        try
        {
            _mundoRepository.Remove(existingMundo);
            await _unitOfWork.CompleteAsync();
            return new MundoResponse(existingMundo);
        }
        catch (Exception e)
        {
            // Do some logging stuff
            return new MundoResponse($"Ocurrio un error al intentar eliminar el mundo: {e.Message}");
        }
    }
}