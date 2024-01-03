using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using JuegoA_API.Juego_A.Domain.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JuegoA_API.Juego_A.Resources;

public class MundoResource
{
    public int Id { get; set; }
    public int Xp { get; set; }
    
    [EnumDataType(typeof(EstadoMundo))]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public EstadoMundo Estado { get; set; }
    
    public string ImagenFondo { get; set; }
    public int SongId { get; set; }
    public string Nombre { get; set; }
    
    // Relaciones
    public int Personaje_Id { get; set; }
}