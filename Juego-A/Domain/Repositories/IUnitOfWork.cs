namespace JuegoA_API.Juego_A.Domain.Repositories;

public interface IUnitOfWork
{
    Task CompleteAsync();
}