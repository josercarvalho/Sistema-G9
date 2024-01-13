using System;
using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;
using SistemaG9.Domain.Interfaces.Services;

namespace SistemaG9.Domain.Services
{
    public class PerfilUsuarioService : ServiceBase<PerfilUsuario>, IPerfilUsuarioService
    {
        private readonly IPerfilUsuarioRepository _repositorioDePerfilDeUsuario;
        public PerfilUsuarioService(IPerfilUsuarioRepository repositorioDePerfilDeUsuario)
            : base(repositorioDePerfilDeUsuario)
        {
            _repositorioDePerfilDeUsuario = repositorioDePerfilDeUsuario;
        }

        public List<Clientes> BuscarClientePerfil(int perfilCliente)
        {
            return _repositorioDePerfilDeUsuario.BuscarClientePerfil(perfilCliente);
        }
    }
}
