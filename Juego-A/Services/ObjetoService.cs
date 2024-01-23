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

    public async Task<Objeto> ReturnById(int id)
    {
        return await _objetoRepository.FindByIdAsync(id);
    }
}