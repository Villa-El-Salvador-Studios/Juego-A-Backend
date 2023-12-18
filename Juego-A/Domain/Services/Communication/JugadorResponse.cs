using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class JugadorResponse : BaseResponse<Jugador>
{
    public JugadorResponse(string message) : base(message)
    {
    }

    public JugadorResponse(Jugador resource) : base(resource)
    {
    }
}