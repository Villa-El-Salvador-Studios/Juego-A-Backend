namespace JuegoA_API.Juego_A.Domain.Models;

public class Jugador
{
    public int Id { get; set; }
    public string Usuario { get; set; }
    public string Contrasenia { get; set; }
    public string fotoPerfil { get; set; }
    public int MundoMaximo { get; set; }

    // Relaciones
    public int? MundoId { get; set; } // Puede ser opcional
    public Mundo Mundo { get; set; }
    
    public IList<Personaje> Personajes = new List<Personaje>();

    public IList<Objeto> Objetos = new List<Objeto>();

    public IList<JugadorHechizo> Hechizos = new List<JugadorHechizo>();
}