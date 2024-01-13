using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IRepositorioBase<Clientes>
    {
        Clientes ListarPorLogin(string login);
        Clientes RecuperarUsuarioPorEmail(string email);
        Clientes LogaCliente(string nome, string senha);
        Clientes CadastraUsuario(Clientes user);
        IEnumerable<Clientes> BuscarPorNome(string nome);
    }
}
