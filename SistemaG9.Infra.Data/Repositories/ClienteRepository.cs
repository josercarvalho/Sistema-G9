using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using System.Linq;
using SistemaG9.Domain.Models;

namespace SistemaG9.Infra.Data.Repositories
{
    public class ClienteRepository : RepositorioBase<Clientes>, IClienteRepository
    {
        public IEnumerable<Clientes> BuscarPorNome(string nome)
        {
            return Db.Cliente.Where(p => p.Nome == nome);
        }
        
        public Clientes CadastraUsuario(Clientes user)
        {
            //user.Senha = user.Senha; // Crypto.EncryptStringAES(user.Senha, user.SenhaKey);
            return Db.Cliente.Add(user);
        }

        public Clientes LogaCliente(string login, string senha)
        {
            var usuario = Db.Cliente.FirstOrDefault(u => u.Login.Equals(login));
            if (usuario == null)
                return null;

            var passDecrypt = usuario.Senha; // Crypto.DecryptStringAES(usuario.Senha, usuario.SenhaKey);

            return passDecrypt == senha ? usuario : null;
        }

        public Clientes RecuperarUsuarioPorEmail(string email)
        {
            var usuario = Db.Cliente.FirstOrDefault(u => u.Email.Equals(email));
            return usuario;
        }

        public Clientes ListarPorLogin(string login)
        {
            return Db.Cliente.FirstOrDefault(p => p.Login.Contains(login));
        }        
    }
}
