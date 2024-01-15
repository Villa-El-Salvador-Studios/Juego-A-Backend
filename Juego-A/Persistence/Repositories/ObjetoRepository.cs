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

    public async Task AddAsync(Objeto objeto)
    {
        await _context.Objetos.AddAsync(objeto);
    }

    public void Remove(Objeto objeto)
    {
        _context.Objetos.Remove(objeto);
    }

    public void Update(Objeto objeto)
    {
        _context.Objetos.Update(objeto);
    }

    public async Task<IEnumerable<Objeto>> GetByJugadorId(int id)
    {
        var objetos = await _context.Objetos
            .Where(o => o.jugadorId == id)
            .ToListAsync();

        return objetos;
    }
}