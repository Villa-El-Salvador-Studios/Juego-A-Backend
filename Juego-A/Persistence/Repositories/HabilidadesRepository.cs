using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class HabilidadesRepository : BaseRepository, IHabilidadesRepository
{
    public HabilidadesRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Habilidades>> ListAsync()
    {
        return await _context.Habilidades.ToListAsync();
    }

    public async Task<Habilidades> FindByIdAsync(int id)
    {
        return await _context.Habilidades.FindAsync(id);
    }

    public async Task AddAsync(Habilidades habilidades)
    {
        await _context.Habilidades.AddAsync(habilidades);
    }

    public void Update(Habilidades habilidades)
    {
        _context.Habilidades.Update(habilidades);
    }

    public void Remove(Habilidades habilidades)
    {
        _context.Habilidades.Remove(habilidades);
    }
}