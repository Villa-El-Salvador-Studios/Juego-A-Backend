namespace JuegoA_API.Juego_A.Domain.Models;

public class Hechizo
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Cooldown { get; set; }
    public string Imagen { get; set; }
    
    // Relaciones

    public IList<JugadorHechizo> Jugadores = new List<JugadorHechizo>();
}