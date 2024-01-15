using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class ObjetoResponse : BaseResponse<Objeto>
{
    public ObjetoResponse(string message) : base(message)
    {
    }

    public ObjetoResponse(Objeto resource) : base(resource)
    {
    }
}