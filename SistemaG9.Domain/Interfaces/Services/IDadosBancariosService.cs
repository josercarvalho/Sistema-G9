using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IDadosBancariosService : IServiceBase<DadosBancarioViewModel>
    {
        IEnumerable<DadosBancarioViewModel> ListarPorCliente(int cliente);
        DadosBancarioViewModel BuscarDadosBancariosRecebedor(int recebedor);
        DadosBancarioViewModel BuscarPorClienteBanco(int cliente, int banco);
    }
}
