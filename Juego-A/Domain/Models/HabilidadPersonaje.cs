namespace JuegoA_API.Juego_A.Domain.Models;

public class HabilidadPersonaje
{
    public int? HabilidadId { get; set; }
    public Habilidad Habilidad { get; set; }
    public int? PersonajeId { get; set; }
    public Personaje Personaje { get; set; }
}