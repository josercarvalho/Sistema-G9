using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IArquivoService : IServiceBase<Arquivos>
    {
        IEnumerable<Arquivos> ListarPorDoador(int clienteId);
        IEnumerable<Arquivos> ListarPorRecebedor(int recebedor);
        IEnumerable<Arquivos> ListarPorStatus(int clienteId, string status);
        Arquivos BuscarPendente(int doador, int recebedor);
        void AtualizaComprovante(int doador, int recebedor);
    }
}
