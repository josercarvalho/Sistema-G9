using System.Collections.Generic;
using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;

namespace SistemaG9.Infra.Data.Repositories
{
    public class EstadoRepository : RepositorioBase<Estados>, IEstadoRepository
    {
        public IEnumerable<Estados> BuscarPorPais(int pais)
        {
            return Db.Estado.Where(p => p.PaisId == pais);
        }
    }
}
