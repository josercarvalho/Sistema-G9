using System;
using SistemaG9.Domain.Interfaces.Repositories;
using System.Data.Entity;
using SistemaG9.Domain.Models;
using System.Linq;

namespace SistemaG9.Infra.Data.Repositories
{
    public class BancoRepository : RepositorioBase<Bancos>, IBancoRepository
    {
        public Bancos BuscarPorBanco(int bancoId)
        {
            return Db.Bancos.FirstOrDefault(x => x.BancoId.Equals(bancoId));
        }

        public Bancos BuscarPorNome(string nome)
        {
            throw new NotImplementedException();
        }
    }
}
