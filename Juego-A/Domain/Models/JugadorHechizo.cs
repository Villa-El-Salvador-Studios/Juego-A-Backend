namespace JuegoA_API.Juego_A.Domain.Models;

public class JugadorHechizo
{
    // Relaciones
    public int? JugadorId { get; set; }
    public Jugador Jugador { get; set; }
    public int? HechizoId { get; set; }
    public Hechizo Hechizo { get; set; }
}