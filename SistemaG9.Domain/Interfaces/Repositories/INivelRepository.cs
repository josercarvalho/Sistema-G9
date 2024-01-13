using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface INivelRepository : IRepositorioBase<Nivel>
    {
        Nivel BuscarPorNivel( int nivel);
        decimal BuscarValorEntrada( int nivel);
        decimal BuscarValorReentrada( int nivel);
        int BuscarEntradas( int nivel);
        int BuscarReentradas( int nivel);
    }
}
