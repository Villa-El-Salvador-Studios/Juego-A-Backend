using System.ComponentModel.DataAnnotations;

namespace JuegoA_API.Juego_A.Resources;

public class VerifyUserResource
{
    [Required]
    public string Usuario { get; set; }
}