namespace JuegoA_API.Juego_A.Domain.Models;

public class Objeto
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public string Imagen { get; set; }
    
    // Relaciones
    
    public IList<JugadorObjeto> Jugadores = new List<JugadorObjeto>();
}