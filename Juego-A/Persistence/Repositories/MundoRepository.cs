using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class MundoRepository : BaseRepository, IMundoRepository
{
    public MundoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Mundo>> ListAsync()
    {
        return await _context.Mundos.ToListAsync();
    }

    public async Task AddAsync(Mundo mundo)
    {
        await _context.Mundos.AddAsync(mundo);
    }

    public void Update(Mundo mundo)
    {
        _context.Mundos.Update(mundo);
    }

    public void Remove(Mundo mundo)
    {
        _context.Mundos.Remove(mundo);
    }

    public async Task<Mundo> FindByNombreAsync(string mundoNombre)
    {
        return await _context.Mundos
            .FirstOrDefaultAsync(j => j.Nombre == mundoNombre);
    }

    public async Task<Mundo> FindByIdAsync(int id)
    {
        return await _context.Mundos.FindAsync(id);
    }
}