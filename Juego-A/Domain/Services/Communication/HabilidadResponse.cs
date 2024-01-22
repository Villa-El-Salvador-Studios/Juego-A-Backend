using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class HabilidadResponse : BaseResponse<Habilidad>
{
    public HabilidadResponse(string message) : base(message)
    {
    }

    public HabilidadResponse(Habilidad resource) : base(resource)
    {
    }
}