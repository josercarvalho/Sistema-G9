using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface INivelService : IServiceBase<Nivel>
    {
        Nivel BuscarPorNivel( int nivel);
        decimal BuscarValorEntrada( int nivel);
        decimal BuscarValorReentrada( int nivel);
        int BuscarEntradas( int nivel);
        int BuscarReentradas( int nivel);
    }
}
