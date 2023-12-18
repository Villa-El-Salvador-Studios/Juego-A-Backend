using JuegoA_API.Shared.Persistence.Contexts;

namespace JuegoA_API.Shared.Persistence.Respositories;

public class BaseRepository
{
    protected readonly AppDbContext _context;

    public BaseRepository(AppDbContext context)
    {
        _context = context;
    }
}