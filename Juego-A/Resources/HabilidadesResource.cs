namespace JuegoA_API.Juego_A.Resources;

public class HabilidadesResource
{
    public int Id { get; set; }
    public string Habilidad1 { get; set; }
    public string Habilidad2 { get; set; }
    public string Habilidad3 { get; set; }
    public string Habilidad4 { get; set; }
    
    // Relaciones
    public int? PersonajeId { get; set; }
}