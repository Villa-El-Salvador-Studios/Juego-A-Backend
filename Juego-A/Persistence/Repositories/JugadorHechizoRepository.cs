using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class JugadorHechizoRepository : BaseRepository, IJugadorHechizoRepository
{
    public JugadorHechizoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<JugadorHechizo>> FindByJugadorIdAsync(int id)
    {
        return await _context.JugadorHechizos
            .Where(jh => jh.JugadorId == id)
            .Include(jh => jh.Hechizo)
            .ToListAsync();
    }

    public async Task<JugadorHechizo> FindByJugadorIdYHechizoIdAsync(int? jugadorHechizoJugadorId, int? jugadorHechizoHechizoId)
    {
        return await _context.JugadorHechizos
            .FirstOrDefaultAsync(jh => jh.JugadorId == jugadorHechizoJugadorId && jh.HechizoId == jugadorHechizoHechizoId);
    }

    public async Task AddAsync(JugadorHechizo jugadorHechizo)
    {
        await _context.JugadorHechizos.AddAsync(jugadorHechizo);
    }
}