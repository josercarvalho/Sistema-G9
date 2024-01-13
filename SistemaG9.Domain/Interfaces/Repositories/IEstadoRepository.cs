using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IEstadoRepository :IRepositorioBase<Estados>
    {
        IEnumerable<Estados> BuscarPorPais(int pais);
    }
}
