using System;
using System.Collections.Generic;
using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Services
{
    public class IClienteService : ServiceBase<Clientes>, Interfaces.Services.IClienteService
    {
        private readonly IClienteRepository _clienteService;
        private readonly IPerfilUsuarioRepository _repositorioPerfil;
        private readonly Interfaces.Repositories.IRedeService _repositorioRede;

        public IClienteService(IClienteRepository repository, Interfaces.Repositories.IRedeService repositorioRede,
                                IPerfilUsuarioRepository repositorioPerfil)
            : base(repository)
        {
            _clienteService = repository;
            _repositorioPerfil = repositorioPerfil;
            _repositorioRede = repositorioRede;
        }

        #region Cliente

        public Clientes ListarPorLogin(string login)
        {
            return _clienteService.ListarPorLogin(login);
        }

        public Clientes RecuperarUsuarioPorEmail(string email)
        {
            return _clienteService.RecuperarUsuarioPorEmail(email);
        }

        public Clientes LogaCliente(string nome, string senha)
        {
            return _clienteService.LogaCliente(nome, senha);
        }

        public Clientes CadastraUsuario(Clientes user)
        {
            try
            {
                return _clienteService.CadastraUsuario(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public IEnumerable<Clientes> BuscarPorNome(string nome)
        {
            return _clienteService.BuscarPorNome(nome);
        }

        public bool IsExistEmail(string email)
        {
            var query = _clienteService.RecuperarUsuarioPorEmail(email);

            return query != null;
        }

        public bool IsExistLogin(string login)
        {
            if (login.Contains("sistema")) return true;

            var query = _clienteService.ListarPorLogin(login);
            return query != null;
        }
        public bool VerificaStatus( int cliente)
        {
            var status = _repositorioRede.GetAll().Where(x => x.ClienteId.Equals(cliente) && x.Status.Equals(0));
            return (status.Count() > 0);
        }
        public void AtualizaStatus(int mattriz, int cliente)
        {
            Clientes novoStatus = _clienteService.GetById(cliente);
            novoStatus.Status = 0;
            _clienteService.Update(novoStatus);
        }

        #endregion

        #region Perfil

        public List<Clientes> RecuperaUsuariosDoPerfil(int perfil)
        {
            return _repositorioPerfil.BuscarClientePerfil(perfil);
        }

        public List<PerfilUsuario> RecuperaTodosPerfisAtivos()
        {
            return _repositorioPerfil.GetAll().Where(x => x.FlAtivo && !x.FlAdminMaster).ToList();
        }

        #endregion
    }
}