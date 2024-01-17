using AutoMapper;
using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Juego_A.Resources;

namespace JuegoA_API.Juego_A.Mapping;

public class ResourceToModelProfile : Profile
{
    public ResourceToModelProfile()
    {
        CreateMap<SaveJugadorResource, Jugador>();
        CreateMap<SavePersonajeResource, Personaje>();
        CreateMap<SaveMundoResource, Mundo>();
        CreateMap<SaveHabilidadesResource, Habilidades>();
        CreateMap<SaveObjetoResource, Objeto>();
        CreateMap<SaveHechizoResource, Hechizo>();
    }
}