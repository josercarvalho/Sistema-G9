using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;
using System.Collections.Generic;
using System.Data.Entity;
using SistemaG9.Domain.Models;
using System;

namespace SistemaG9.Infra.Data.Repositories
{
    public class ArquivoRepository : RepositorioBase<Arquivos>, IArquivoRepository
    {
        public Arquivos BuscarPendente(int doador, int recebedor)
        {
            return Db.Arquivo.FirstOrDefault(x => x.ClienteId.Equals(doador) && x.Recebedor.Equals(recebedor));
        }

        public IEnumerable<Arquivos> ListarPorCliente(int cliente)
        {
            return Db.Arquivo.Where(p => p.ClienteId == cliente);
        }

        public IEnumerable<Arquivos> ListarPorDoador(int doador)
        {
            return Db.Arquivo.Include(x=>x.Cliente)
                             .Where(x=>x.ClienteId.Equals(doador));
        }

        public IEnumerable<Arquivos> ListarPorRecebedor(int recebedor)
        {
            return Db.Arquivo.Include(x => x.Cliente)
                             .Where(x => x.Recebedor.Equals(recebedor));
        }

        public IEnumerable<Arquivos> ListarPorStatus(int clienteId, string status)
        {
            return Db.Arquivo.Include(x => x.Cliente)
                             .Where(x => x.Recebedor.Equals(clienteId) && x.Status.Equals(status));
        }
    }
}
