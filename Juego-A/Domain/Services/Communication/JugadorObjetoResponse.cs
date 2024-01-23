using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class JugadorObjetoResponse : BaseResponse<JugadorObjeto>
{
    public JugadorObjetoResponse(string message) : base(message)
    {
    }

    public JugadorObjetoResponse(JugadorObjeto resource) : base(resource)
    {
    }
}