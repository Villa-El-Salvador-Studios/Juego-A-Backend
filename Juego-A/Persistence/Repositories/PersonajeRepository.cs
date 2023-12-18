using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class PersonajeRepository : BaseRepository, IPersonajeRepository
{
    public PersonajeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Personaje>> ListAsync()
    {
        return await _context.Personajes.ToListAsync();
    }

    public async Task<Personaje> FindByIdAsync(int id)
    {
        return await _context.Personajes.FindAsync(id);
    }

    public async Task AddAsync(Personaje personaje)
    {
        await _context.Personajes.AddAsync(personaje);
    }

    public void Update(Personaje personaje)
    {
        _context.Personajes.Update(personaje);
    }

    public void Remove(Personaje personaje)
    {
        _context.Personajes.Remove(personaje);
    }
}