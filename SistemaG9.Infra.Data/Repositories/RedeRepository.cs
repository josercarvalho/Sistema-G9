using System.Collections.Generic;
using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;
using System.Data.Entity;

namespace SistemaG9.Infra.Data.Repositories
{
    public class RedeRepository : RepositorioBase<Rede>, IRedeService
    {
        public IEnumerable<Rede> ListarMinhaRede(int posicao)
        {
            return Db.Rede.Include(x => x.Cliente).Include(x => x.Patrocinador)
                          .Where(x => x.Posicao >= posicao);
        }

        public Rede BuscarPorApelido(string apelido)
        {
            return Db.Rede.FirstOrDefault(x => x.Apelido.Equals(apelido));
        }

        public IEnumerable<Rede> ListarPorCliente(int cliente)
        {
            return Db.Rede.Include(x => x.Cliente).Include(x => x.Patrocinador)
                          .Where(x => x.ClienteId.Equals(cliente)).OrderBy(x => x.Posicao);
        }

        public IEnumerable<Rede> ListarPorLogin(string login)
        {
            return Db.Rede.Include(x => x.Cliente).Include(x => x.Patrocinador)
                          .Where(x => x.Login.Equals(login));
        }

        public Rede BuscarApelido(string apelidoRecebedor)
        {
            var firstOrDefault = Db.Rede.FirstOrDefault(x => x.Apelido.Equals(apelidoRecebedor));
            if (firstOrDefault != null)
            {
                var posicao = firstOrDefault.Posicao;

                return Db.Rede.FirstOrDefault(x => x.Posicao.Equals(posicao + 1));
            }
            return null;
        }

        public IEnumerable<Rede> ListarRecebedores(string apelido)
        {
            return Db.Rede.Where(x => x.Login.Equals(apelido)).OrderByDescending(x => x.Posicao);
        }

        public IEnumerable<Rede> ListarRede(int matriz)
        {
            return Db.Rede.Include(x => x.Cliente).Include(x => x.Patrocinador)
                          .Where(x => x.MatrizId.Equals(matriz)).OrderBy(x => x.Posicao);
        }

        public Rede BuscarStatus(string apelido)
        {
            return Db.Rede.FirstOrDefault(x => x.Apelido.Equals(apelido));
        }

        public Rede UltimoDaRede()
        {
            return Db.Rede.OrderByDescending(x => x.Posicao).First();
        }
    }
}