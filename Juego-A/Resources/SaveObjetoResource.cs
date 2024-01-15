namespace JuegoA_API.Juego_A.Resources;

public class SaveObjetoResource
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public int Cantidad { get; set; }
    public string Imagen { get; set; }
    
    // Relaciones
    public int? jugadorId { get; set; }
}