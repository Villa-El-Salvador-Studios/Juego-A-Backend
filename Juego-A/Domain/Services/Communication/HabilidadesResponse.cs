using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class HabilidadesResponse : BaseResponse<Habilidades>
{
    public HabilidadesResponse(string message) : base(message)
    {
    }

    public HabilidadesResponse(Habilidades resource) : base(resource)
    {
    }
}