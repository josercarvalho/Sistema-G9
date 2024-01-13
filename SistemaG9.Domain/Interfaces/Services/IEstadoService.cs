using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IEstadoService : IServiceBase<Estados>
    {
        IEnumerable<Estados> BuscaEstados();
    }
}
