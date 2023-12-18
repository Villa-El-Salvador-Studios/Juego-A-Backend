﻿using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Resources;

namespace JuegoA_API.Juego_A.Mapping;

public class ModelToResourceProfile : Profile
{
    public ModelToResourceProfile()
    {
        CreateMap<Jugador, JugadorResource>();
        CreateMap<Personaje, PersonajeResource>();
    }
}