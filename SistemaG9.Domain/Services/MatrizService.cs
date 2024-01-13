using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Services
{
    public class MatrizService : ServiceBase<Matriz>, IMatrizService
    {
        private readonly IMatrizRepositorio _repositorioMatriz;

        public MatrizService(IMatrizRepositorio repository) 
            : base(repository)
        {
            _repositorioMatriz = repository;
        }
    }
}
