namespace JuegoA_API.Juego_A.Domain.Models;

public class JugadorObjeto
{
    public int Cantidad { get; set; }
    
    // Relaciones
    
    public int? JugadorId { get; set; }
    public Jugador Jugador { get; set; }
    public int? ObjetoId { get; set; }
    public Objeto Objeto { get; set; }
}