namespace JuegoA_API.Juego_A.Domain.Models;

public class Personaje
{
    public int Id { get; set; }
    public int Vida { get; set; }
    public int Nivel { get; set; }
    public string Nombre { get; set; }
    public int Ataque { get; set; }
    public int Experiencia { get; set; }
    public string Imagen { get; set; }
    
    // Relaciones
    public int JugadorId { get; set; }
    public Jugador Jugador { get; set; }
}