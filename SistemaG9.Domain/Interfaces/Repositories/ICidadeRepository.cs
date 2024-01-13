using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface ICidadeRepository : IRepositorioBase<Cidades>
    {
        IEnumerable<Cidades> BuscarPorEstado(int estado);
        Cidades BuscarPorNome(string nome);
    }
}
