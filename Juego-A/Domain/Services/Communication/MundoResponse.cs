using JuegoA_API.Juego_A.Domain.Models;
using JuegoA_API.Shared.Domain.Services.Communication;

namespace JuegoA_API.Juego_A.Domain.Services.Communication;

public class MundoResponse : BaseResponse<Mundo>
{
    public MundoResponse(string message) : base(message)
    {
    }

    public MundoResponse(Mundo resource) : base(resource)
    {
    }
}