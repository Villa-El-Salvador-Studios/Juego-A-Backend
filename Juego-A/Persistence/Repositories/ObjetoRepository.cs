using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class ObjetoRepository : BaseRepository, IObjetoRepository
{
    public ObjetoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Objeto>> ListAsync()
    {
        return await _context.Objetos.ToListAsync();
    }

    public async Task<Objeto> FindByIdAsync(int id)
    {
        return await _context.Objetos.FindAsync(id);
    }
}