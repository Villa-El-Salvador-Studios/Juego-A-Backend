namespace JuegoA_API.Juego_A.Resources;

public class JugadorObjetoResource
{
    public int Cantidad { get; set; }
    
    // Relaciones
    
    public int? JugadorId { get; set; }
    public int? ObjetoId { get; set; }
}