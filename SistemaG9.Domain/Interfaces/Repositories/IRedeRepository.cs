using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IRedeService : IRepositorioBase<Rede>
    {
        Rede BuscarPorApelido( string apelido);
        Rede BuscarApelido( string apelidoRecebedor);
        Rede BuscarStatus( string apelido);
        Rede UltimoDaRede();
        IEnumerable<Rede> ListarPorCliente( int cliente);
        IEnumerable<Rede> ListarRede(int matriz);
        IEnumerable<Rede> ListarPorLogin( string login);
        IEnumerable<Rede> ListarMinhaRede( int posicao);
        IEnumerable<Rede> ListarRecebedores( string apelido);
    }
}
