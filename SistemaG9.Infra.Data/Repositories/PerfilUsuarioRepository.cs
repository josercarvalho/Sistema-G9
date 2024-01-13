using System.Collections.Generic;
using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Models;

namespace SistemaG9.Infra.Data.Repositories
{
    public class PerfilUsuarioRepository : RepositorioBase<PerfilUsuario>, IPerfilUsuarioRepository
    {
        public List<Clientes> BuscarClientePerfil(int perfilCliente)
        {
            var perfil = Db.PerfilUsuario.FirstOrDefault(x => x.PerfilUsuarioId == perfilCliente);
            return perfil != null ? perfil.Clientes.ToList() : null;
        }
    }
}
