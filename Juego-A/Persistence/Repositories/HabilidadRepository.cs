using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class HabilidadRepository : BaseRepository, IHabilidadRepository
{
    public HabilidadRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Habilidad>> ListAsync()
    {
        return await _context.Habilidades.ToListAsync();
    }

    public async Task<Habilidad> FindByIdAsync(int id)
    {
        return await _context.Habilidades.FindAsync(id);
    }
}