using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IDoacaoService : IServiceBase<Doacoes>
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
        void AtualizaStatusDoador(int doador);
        void AtualizaStatusRecebedor(int recebedor);
        void AtualizaStatusPagamento(int nivel, string apelidoDoador, string apelidoRecebedor);
        void ExecutaDoacao(decimal valor, int nivel, int clienteId, string doador, string recebedor, int EntradaReentra);
        bool GetComprovante(int cliente, string comprovante);
    }
}
