using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Juego_A.Domain.Services;
using JuegoA_API.Juego_A.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Services;

public class HabilidadService : IHabilidadService
{
    private readonly IHabilidadRepository _habilidadRepository;
    private readonly IUnitOfWork _unitOfWork;

    public HabilidadService(IHabilidadRepository habilidadRepository, IUnitOfWork unitOfWork)
    {
        _habilidadRepository = habilidadRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Habilidad>> ListAsync()
    {
        return await _habilidadRepository.ListAsync();
    }

    public async Task<Habilidad> ReturnById(int id)
    {
        return await _habilidadRepository.FindByIdAsync(id);
    }
}