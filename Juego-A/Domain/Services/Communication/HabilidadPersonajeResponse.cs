using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class HabilidadPersonajeResponse : BaseResponse<HabilidadPersonaje>
{
    public HabilidadPersonajeResponse(string message) : base(message)
    {
    }

    public HabilidadPersonajeResponse(HabilidadPersonaje resource) : base(resource)
    {
    }
}