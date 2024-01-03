using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JuegoA_API.Juego_A.Domain.Models;

namespace JuegoA_API.Juego_A.Resources;

public class SaveMundoResource
{
    public int Xp { get; set; }
    
    [EnumDataType(typeof(EstadoMundo))]
    public EstadoMundo Estado { get; set; }
    
    public string ImagenFondo { get; set; }
    public int SongId { get; set; }
    public string Nombre { get; set; }
    
    // Relaciones
    public int Personaje_Id { get; set; }
}