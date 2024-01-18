using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class JugadorHechizoResponse : BaseResponse<JugadorHechizo>
{
    public JugadorHechizoResponse(string message) : base(message)
    {
    }

    public JugadorHechizoResponse(JugadorHechizo resource) : base(resource)
    {
    }
}