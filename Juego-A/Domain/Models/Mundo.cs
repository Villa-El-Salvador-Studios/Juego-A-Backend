namespace JuegoA_API.Juego_A.Domain.Models;

public class Mundo
{
    public int Id { get; set; }
    public int Xp { get; set; }
    public EstadoMundo Estado { get; set; }
    
    // Relaciones
    public int Personaje_Id { get; set; }
    public Personaje Personaje { get; set; }
    
    public Jugador Jugador { get; set; }
}