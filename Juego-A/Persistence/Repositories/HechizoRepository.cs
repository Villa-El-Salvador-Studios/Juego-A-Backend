using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class HechizoRepository : BaseRepository, IHechizoRepository
{
    public HechizoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Hechizo>> ListAsync()
    {
        return await _context.Hechizos.ToListAsync();
    }

    public async Task<Hechizo> FindByIdAsync(int id)
    {
        return await _context.Hechizos.FindAsync(id);
    }

    public void Update(Hechizo hechizo)
    {
        _context.Hechizos.Update(hechizo);
    }
}