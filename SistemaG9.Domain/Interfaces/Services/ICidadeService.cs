using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface ICidadeService : IServiceBase<Cidades>
    {
        IEnumerable<Cidades> BuscarPorEstado(int estado);
    }
}
