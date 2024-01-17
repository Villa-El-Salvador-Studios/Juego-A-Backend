using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class HechizoResponse : BaseResponse<Hechizo>
{
    public HechizoResponse(string message) : base(message)
    {
    }

    public HechizoResponse(Hechizo resource) : base(resource)
    {
    }
}