using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Services
{
    public class CidadeService : ServiceBase<Cidades>, ICidadeService
    {
        private readonly ICidadeRepository _cidadeRepository;

        public CidadeService(ICidadeRepository cidadeRepository)
            : base(cidadeRepository)
        {
            _cidadeRepository = cidadeRepository;
        }
        
        public IEnumerable<Cidades> BuscarPorEstado(int estado)
        {
            return _cidadeRepository.BuscarPorEstado(estado);
        }
    }
}
