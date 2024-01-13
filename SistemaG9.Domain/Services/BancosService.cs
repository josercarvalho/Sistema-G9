using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using System;
using SistemaG9.Domain.Interfaces.Repositories;

namespace SistemaG9.Domain.Services
{
    public class BancosService : ServiceBase<Bancos>, IBancoService
    {
        private readonly IBancoRepository _repoBanco;
        public BancosService(IBancoRepository repository) : base(repository)
        {
            _repoBanco = repository;
        }

        public Bancos BuscarPorBanco(int bancoId)
        {
            return _repoBanco.BuscarPorBanco(bancoId);
        }

        public Bancos BuscarPorNome(string nome)
        {
            return _repoBanco.BuscarPorNome(nome);
        }
    }
}
