using System.Linq;
using System.Data.Entity;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;
using System.Collections.Generic;
using System;

namespace SistemaG9.Infra.Data.Repositories
{
    public class DadosBancariosRepository : RepositorioBase<DadosBancarioViewModel>, IDadosBancariosRepository
    {
        public DadosBancarioViewModel BuscarDadosBancariosRecebedor(int recebedor)
        {
            return Db.DadoBancarios.Include(x => x.Cliente).Include(x => x.Banco).FirstOrDefault(x => x.ClienteId.Equals(recebedor));
        }

        public DadosBancarioViewModel BuscarPorClienteBanco(int cliente, int banco)
        {
            return Db.DadoBancarios.Include(x => x.Banco).Include(x => x.Cliente).FirstOrDefault(x => x.ClienteId.Equals(cliente) && x.BancoId.Equals(banco));
        }

        public IEnumerable<DadosBancarioViewModel> ListarPorCliente(int cliente)
        {
            return Db.DadoBancarios.Include(x => x.Banco).Include(x => x.Cliente).Where(x => x.ClienteId.Equals(cliente)); 
        }
    }
}
