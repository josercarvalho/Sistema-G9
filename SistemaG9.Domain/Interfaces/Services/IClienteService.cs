using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IClienteService : IServiceBase<Clientes>
    {
        #region Cliente

        Clientes ListarPorLogin(string login);
        Clientes RecuperarUsuarioPorEmail(string email);
        Clientes LogaCliente(string nome, string senha);
        Clientes CadastraUsuario(Clientes user);
        IEnumerable<Clientes> BuscarPorNome(string nome);
        bool IsExistEmail(string email);
        bool IsExistLogin(string login);
        bool VerificaStatus( int cliente);
        void AtualizaStatus(int mattriz, int cliente);

        #endregion

        #region PerfilUsuario

        List<Clientes> RecuperaUsuariosDoPerfil(int perfil);
        List<PerfilUsuario> RecuperaTodosPerfisAtivos();

        #endregion

    }
}
