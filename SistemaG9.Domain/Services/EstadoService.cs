using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;

namespace SistemaG9.Domain.Services
{
    public class EstadoService : ServiceBase<Estados>, IEstadoService
    {
        private readonly IEstadoRepository _servEstado;
        public EstadoService(IEstadoRepository repository) : base(repository)
        {
            _servEstado = repository;
        }

        public IEnumerable<Estados> BuscaEstados()
        {
            return _servEstado.GetAll().OrderBy(x=>x.Nome);
        }
    }
}
