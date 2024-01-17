using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class HechizoService : IHechizoService
{
    private readonly IHechizoRepository _hechizosRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HechizoService(IHechizoRepository hechizosRepository, IUnitOfWork unitOfWork)
    {
        _hechizosRepository = hechizosRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Hechizo>> ListAsync()
    {
        return await _hechizosRepository.ListAsync();
    }

    public async Task<Hechizo> ReturnById(int id)
    {
        return await _hechizosRepository.FindByIdAsync(id);
    }

    public async Task<HechizoResponse> UpdateAsync(int id, Hechizo hechizo)
    {
        var existingHechizo = await _hechizosRepository.FindByIdAsync(id);
        
        if (existingHechizo == null)
            return new HechizoResponse("Hechizo no encontrado.");

        existingHechizo.Cooldown = hechizo.Cooldown;
        
        try
        {
            _hechizosRepository.Update(existingHechizo);
            await _unitOfWork.CompleteAsync();
            return new HechizoResponse(existingHechizo);
        }
        catch (Exception e)
        {
            return new HechizoResponse($"Ocurrio un error al actualizar el hechizo: {e.Message}");
        }
    }
}