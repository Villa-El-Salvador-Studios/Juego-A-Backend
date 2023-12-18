using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class PersonajeResponse : BaseResponse<Personaje>
{
    public PersonajeResponse(string message) : base(message)
    {
    }

    public PersonajeResponse(Personaje resource) : base(resource)
    {
    }
}