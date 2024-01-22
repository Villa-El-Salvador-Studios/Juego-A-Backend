using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class HabilidadPersonajeRepository : BaseRepository, IHabilidadPersonajeRepository
{
    public HabilidadPersonajeRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<HabilidadPersonaje>> FindByPersonajeIdAsync(int personajeId)
    {
        return await _context.HabilidadPersonajes
            .Where(hp => hp.PersonajeId == personajeId)
            .Include(hp => hp.Habilidad)
            .ToListAsync();
    }

    public async Task<HabilidadPersonaje> FindByHabilidadIdYPersonajeIdAsync(int? habilidadId, int? personajeId)
    {
        return await _context.HabilidadPersonajes
            .FirstOrDefaultAsync(hp => hp.HabilidadId == habilidadId && hp.PersonajeId == personajeId);
    }

    public async Task AddAsync(HabilidadPersonaje habilidadPersonaje)
    {
        await _context.HabilidadPersonajes.AddAsync(habilidadPersonaje);
    }
}