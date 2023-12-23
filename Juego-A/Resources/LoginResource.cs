using System.ComponentModel.DataAnnotations;

namespace JuegoA_API.Juego_A.Resources;

public class LoginResource
{
    [Required]
    public string Usuario { get; set; }

    [Required]
    public string Contraseña { get; set; }
}