namespace JuegoA_API.Juego_A.Domain.Models;

public class Habilidad
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string RutaAudio { get; set; }
    
    // Relaciones
    public IList<HabilidadPersonaje> Personajes = new List<HabilidadPersonaje>();
}