using System.Collections.Generic;
using System.Linq;
using SistemaG9.Domain.Interfaces.Repositories;
using System.Data.Entity;
using SistemaG9.Domain.Models;

namespace SistemaG9.Infra.Data.Repositories
{
    public class DoacoesRepository : RepositorioBase<Doacoes>, IDoacoesRepository
    {
        public IEnumerable<Doacoes> ListarDoacoesPendentes(string cliente)
        {
            return Db.Doacao.Include(x => x.Cliente)
                            .Where(p => p.Recebedor.Equals(cliente) && p.Status.Equals(0));
        }

        public IEnumerable<Doacoes> ListarDoacoesEnviadas(string cliente)
        {
            return Db.Doacao.Include(x => x.Cliente)
                            .Where(p => p.Recebedor.Equals(cliente) && p.Status.Equals(1));
        }

        public IEnumerable<Doacoes> ListarDoacoesRecebidas(string cliente)
        {
            return Db.Doacao.SqlQuery("SELECT * FROM Doacao WHERE Recebedor IN (" + cliente + ") AND status=2", cliente);
        }

        public IEnumerable<Doacoes> ListarDoacoesRealizadas(int cliente)
        {
            return Db.Doacao.Include(x => x.Cliente)
                            .Where(p => p.ClienteId.Equals(cliente) && p.Status.Equals(2));
        }

        public IEnumerable<Doacoes> ListarPorDoador(string doador)
        {
            return Db.Doacao.Include(x => x.Cliente).Where(p => p.Doador.Equals(doador));
        }

        public IEnumerable<Doacoes> ListarPorRecebedor(string recebedor)
        {
            //return Db.Doacao.Include(x => x.Cliente).Where(p => p.Recebedor.Equals(recebedor));
            return Db.Doacao.SqlQuery("SELECT * FROM Doacao WHERE Recebedor = '" + recebedor + "' ORDER BY Nivel, ClienteId", recebedor);
            //return Db.Doacao.SqlQuery("SELECT A.[Nivel] as Nivel, A.[ClienteId] as ClienteId, A.[Doador] as Doador, A.[Recebedor] as Recebedor, A.[Tipo] as Tipo, A.[Valor] as Valor, A.[DataCadastro] as DataCadastro, C.[Nome] as Nome, B.[Status] as Status WHERE A.[Doador]=B.[Apelido] AND A.[ClienteId]=C.[ClienteId] AND A.[Recebedor] = '" + recebedor + "' ORDER BY Nivel, ClienteId", recebedor);
        }

        public decimal SomarDoacoes(int nivel, string apelido)
        {
            var retorno = Db.Doacao.Where(p => p.Nivel.Equals(nivel) && p.Recebedor.Equals(apelido) && p.Status.Equals(2)).ToList();
            return retorno.Sum(x => x.Valor);
        }

        public int BuscarEntradas(int nivel, string apelido)
        {
            return Db.Doacao.Count(p => p.Nivel.Equals(nivel) && p.Recebedor.Equals(apelido) && p.Tipo.Equals(1) && p.Status.Equals(2));
        }

        public int ConfirmaRecebimento(int nivel, string cliente)
        {
            return Db.Doacao.Count(x => x.Nivel.Equals(nivel) && x.Recebedor.Equals(cliente) && x.Status.Equals(2));
        }

        public Doacoes BuscarDoacaoPendente(int nivel, string doador, string recebedor, int status)
        {
            return Db.Doacao.FirstOrDefault(x => x.Nivel.Equals(nivel) && x.Doador.Equals(doador) && x.Recebedor.Equals(recebedor) && x.Status.Equals(status));
        }

        public IEnumerable<Doacoes> BuscarEmAberto(int cliente)
        {
            return Db.Doacao.Include(x => x.Cliente).Include(x=>x.DadosBancarios)
                            .Where(p => p.ClienteId.Equals(cliente) && p.Status.Equals(0));
        }

        public IEnumerable<Doacoes> BuscarPorDoadorPendente(string doador)
        {
            return Db.Doacao.Include(x => x.Cliente)
                            .Where(p => p.Doador.Equals(doador) && p.Status.Equals(1));
        }

        public bool GetComprovante(int cliente, string comprovante)
        {
            return (Db.Doacao.Count(x => x.ClienteId.Equals(cliente) && x.Comprovante.Equals(comprovante)) > 0);
        }
    }
}