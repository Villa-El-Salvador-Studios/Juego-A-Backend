namespace JuegoA_API.Juego_A.Domain.Models;

public class Habilidades
{
    public int Id { get; set; }
    public string Habilidad1 { get; set; }
    public string Habilidad2 { get; set; }
    public string Habilidad3 { get; set; }
    public string Habilidad4 { get; set; }
    
    // Relaciones
    public int? PersonajeId { get; set; }
    public Personaje Personaje { get; set; }
}