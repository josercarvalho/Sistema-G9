using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IRedeService : IServiceBase<Rede>
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
        IEnumerable<Rede> ListarDoacoesPendentes();
        void CadastoNaRede(int id, int nivel, string login, string apelido);
        void AtualizaNivelRede( int nivel, string doador);
        void AtualizaStatusDoador( string doador);
    }
}
