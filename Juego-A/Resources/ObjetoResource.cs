namespace JuegoA_API.Juego_A.Resources;

public class ObjetoResource
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Cantidad { get; set; }
    public string Imagen { get; set; }
    
    // Relaciones
    public int? jugadorId { get; set; }
}