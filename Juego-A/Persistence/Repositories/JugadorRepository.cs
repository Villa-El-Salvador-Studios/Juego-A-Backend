using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class JugadorRepository : BaseRepository, IJugadorRepository
{
    public JugadorRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<IEnumerable<Jugador>> ListAsync()
    {
        return await _context.Jugadores.ToListAsync();
    }

    public async Task AddAsync(Jugador jugador)
    {
        await _context.Jugadores.AddAsync(jugador);
    }

    public void Update(Jugador jugador)
    {
        _context.Jugadores.Update(jugador);
    }

    public void Remove(Jugador jugador)
    {
        _context.Jugadores.Remove(jugador);
    }
}