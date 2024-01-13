using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IDoacoesRepository :IRepositorioBase<Doacoes>
    {
        IEnumerable<Doacoes> BuscarEmAberto( int cliente);
        IEnumerable<Doacoes> ListarDoacoesRecebidas( string cliente);
        IEnumerable<Doacoes> ListarDoacoesPendentes( string cliente);
        IEnumerable<Doacoes> ListarDoacoesEnviadas( string cliente);
        IEnumerable<Doacoes> ListarDoacoesRealizadas( int cliente);
        IEnumerable<Doacoes> ListarPorDoador( string doador);
        IEnumerable<Doacoes> BuscarPorDoadorPendente( string doador);
        IEnumerable<Doacoes> ListarPorRecebedor( string recebedor);
        decimal SomarDoacoes( int nivel, string apelido);
        int BuscarEntradas( int nivel, string apelido);
        int ConfirmaRecebimento(int nivel, string cliente);
        Doacoes BuscarDoacaoPendente(int nivel, string doador, string recebedor, int status);
        bool GetComprovante(int cliente, string comprovante);
    }
}
