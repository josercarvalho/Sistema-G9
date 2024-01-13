using System;
using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;
using System.Linq;

namespace SistemaG9.Infra.Data.Repositories
{
    public class CidadeRepository : RepositorioBase<Cidades>, ICidadeRepository
    {
        public IEnumerable<Cidades> BuscarPorEstado(int estado)
        {
            return Db.Cidade.Where(x => x.EstadoId.Equals(estado)).OrderBy(x => x.Nome);
        }

        public Cidades BuscarPorNome(string nome)
        {
            return Db.Cidade.FirstOrDefault(x => x.Nome.Equals(nome));
        }
    }
}
