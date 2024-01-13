using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Services
{
    public class NivelService : ServiceBase<Nivel>, INivelService
    {
        private readonly INivelRepository _repoNivel;

        public NivelService(INivelRepository repository)
            : base(repository)
        {
            _repoNivel = repository;
        }

        public int BuscarEntradas(int nivel)
        {
            return _repoNivel.BuscarEntradas(nivel);
        }

        public Nivel BuscarPorNivel(int nivel)
        {
            return _repoNivel.BuscarPorNivel(nivel);
        }

        public int BuscarReentradas(int nivel)
        {
            return _repoNivel.BuscarReentradas(nivel);
        }

        public decimal BuscarValorEntrada(int nivel)
        {
            return _repoNivel.BuscarValorEntrada(nivel);
        }

        public decimal BuscarValorReentrada(int nivel)
        {
            return _repoNivel.BuscarValorReentrada(nivel);
        }
    }
}
