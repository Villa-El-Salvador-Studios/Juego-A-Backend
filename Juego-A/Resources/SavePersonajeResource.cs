namespace JuegoA_API.Juego_A.Resources;

public class SavePersonajeResource
{
    public int Vida { get; set; }
    public int Nivel { get; set; }
    public string Nombre { get; set; }
    public int Ataque { get; set; }
    public int Experiencia { get; set; }
    public string Imagen { get; set; }
    public int JugadorId { get; set; }
}