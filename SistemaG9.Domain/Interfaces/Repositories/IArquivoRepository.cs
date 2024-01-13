using System.Collections.Generic;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IArquivoRepository : IRepositorioBase<Arquivos>
    {
        IEnumerable<Arquivos> ListarPorDoador(int clienteId);
        IEnumerable<Arquivos> ListarPorRecebedor(int recebedor);
        IEnumerable<Arquivos> ListarPorStatus(int clienteId, string status);
        Arquivos BuscarPendente(int doador, int recebedor);
    }
}
