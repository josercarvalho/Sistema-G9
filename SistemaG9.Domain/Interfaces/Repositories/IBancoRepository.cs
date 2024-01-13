using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Interfaces.Repositories
{
    public interface IBancoRepository : IRepositorioBase<Bancos>
    {
        Bancos BuscarPorBanco(int bancoId);
        Bancos BuscarPorNome(string nome);
    }
}
