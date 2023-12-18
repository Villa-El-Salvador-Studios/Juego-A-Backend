namespace JuegoA_API.Juego_A.Domain.Models;

public class Jugador
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Contrasenia { get; set; }
    public string fotoPerfil { get; set; }

    // Relaciones
    public IList<Personaje> Personajes = new List<Personaje>();
}