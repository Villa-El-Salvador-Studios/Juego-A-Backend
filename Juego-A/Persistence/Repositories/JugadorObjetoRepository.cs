using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Domain.Repositories;
using JuegoA_API.Shared.Persistence.Contexts;
using JuegoA_API.Shared.Persistence.Respositories;
using Microsoft.EntityFrameworkCore;

namespace JuegoA_API.Juego_A.Persistence.Repositories;

public class JugadorObjetoRepository : BaseRepository, IJugadorObjetoRepository
{
    public JugadorObjetoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<JugadorObjeto> FindByJugadorIdAndObjetoId(int? jugadorId, int? objetoId)
    {
        return await _context.JugadorObjetos
            .FirstOrDefaultAsync(jo => jo.JugadorId == jugadorId && jo.ObjetoId == objetoId);
    }

    public async Task AddAsync(JugadorObjeto jugadorObjeto)
    {
        await _context.JugadorObjetos.AddAsync(jugadorObjeto);
    }

    public void Update(JugadorObjeto jugadorObjeto)
    {
        _context.JugadorObjetos.Update(jugadorObjeto);
    }
}