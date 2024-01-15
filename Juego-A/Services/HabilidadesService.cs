using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;

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
}