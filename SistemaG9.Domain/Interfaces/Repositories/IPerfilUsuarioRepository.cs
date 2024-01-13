using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IPerfilUsuarioRepository : IRepositorioBase<PerfilUsuario>
    {
        List<Clientes> BuscarClientePerfil(int perfilCliente);
    }
}
