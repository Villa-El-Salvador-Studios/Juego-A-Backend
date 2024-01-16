using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class HabilidadesService : IHabilidadesService
{
    private readonly IHabilidadesRepository _habilidadesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HabilidadesService(IHabilidadesRepository habilidadesRepository, IUnitOfWork unitOfWork)
    {
        _habilidadesRepository = habilidadesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Habilidades>> ListAsync()
    {
        return await _habilidadesRepository.ListAsync();
    }

    public async Task<Habilidades> ReturnById(int id)
    {
        return await _habilidadesRepository.FindByIdAsync(id);
    }

    public async Task<HabilidadesResponse> UpdateAsync(int id, Habilidades habilidades)
    {
        var existingHabilidades = await _habilidadesRepository.FindByIdAsync(id);
        
        if (existingHabilidades == null)
            return new HabilidadesResponse("Habilidades no encontradas.");

        existingHabilidades.Habilidad1 = habilidades.Habilidad1;
        existingHabilidades.Habilidad2 = habilidades.Habilidad2;
        existingHabilidades.Habilidad3 = habilidades.Habilidad3;
        existingHabilidades.Habilidad4 = habilidades.Habilidad4;
        
        try
        {
            _habilidadesRepository.Update(existingHabilidades);
            await _unitOfWork.CompleteAsync();
            return new HabilidadesResponse(existingHabilidades);
        }
        catch (Exception e)
        {
            return new HabilidadesResponse($"Ocurrio un error al actualizar las habilidades: {e.Message}");
        }
    }
}