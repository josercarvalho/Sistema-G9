using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Services
{
    public interface IBancoService : IServiceBase<Bancos>
    {
        Bancos BuscarPorBanco(int bancoId);
        Bancos BuscarPorNome(string nome);
    }
}
