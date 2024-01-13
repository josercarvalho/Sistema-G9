using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;
using System.Linq;

namespace SistemaG9.Infra.Data.Repositories
{
    public class NivelRepository : RepositorioBase<Nivel>, INivelRepository
    {
        public int BuscarEntradas(int nivel)
        {
            return Db.Niveis.FirstOrDefault(x => x.NivelMatriz.Equals(nivel)).Entradas;
        }

        public Nivel BuscarPorNivel(int nivel)
        {
            return Db.Niveis.FirstOrDefault(x => x.MatrizId.Equals(1) && x.NivelId.Equals(nivel));
        }

        public int BuscarReentradas(int nivel)
        {
            return Db.Niveis.FirstOrDefault(x => x.NivelMatriz.Equals(nivel)).Reentradas;
        }

        public decimal BuscarValorEntrada(int nivel)
        {
            var retorno = Db.Niveis.Where(x => x.NivelMatriz.Equals(nivel)).First();
            return retorno.ValorEntrada;
        }

        public decimal BuscarValorReentrada(int nivel)
        {
            var retorno = Db.Niveis.Where(x => x.NivelMatriz.Equals(nivel)).First();
            return retorno.ValorReentrada;
        }
    }
}
