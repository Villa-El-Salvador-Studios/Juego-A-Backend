﻿namespace JuegoA_API.Juego_A.Resources;

public class SaveJugadorResource
{
    public string Usuario { get; set; }
    public string Contrasenia { get; set; }
    public string fotoPerfil { get; set; }
    public int MundoMaximo { get; set; }
    public int? MundoId { get; set; }
}