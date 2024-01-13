using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using System;

namespace SistemaG9.Domain.Services
{
    public class DoacaoService : ServiceBase<Doacoes>, IDoacaoService
    {
        private readonly IDoacoesRepository _servDoar;
        private readonly IClienteRepository _repositorioCliente;
        private readonly Interfaces.Repositories.IRedeService _repositorioRede;
        private readonly INivelRepository _repositorioNivel;

        public DoacaoService(IDoacoesRepository repository,
                             Interfaces.Repositories.IRedeService repositorioRede,
                             INivelRepository repositorioNivel,
                             IClienteRepository repositorioCliente)
            : base(repository)
        {
            _servDoar = repository;
            _repositorioRede = repositorioRede;
            _repositorioNivel = repositorioNivel;
            _repositorioCliente = repositorioCliente;
        }

        public int BuscarEntradas(int nivel, string apelido)
        {
            return _servDoar.BuscarEntradas(nivel, apelido);
        }

        public IEnumerable<Doacoes> ListarDoacoesPendentes(string cliente)
        {
            return _servDoar.ListarDoacoesPendentes(cliente);
        }

        public IEnumerable<Doacoes> ListarPorDoador(string doador)
        {
            return _servDoar.ListarPorDoador(doador);
        }

        public IEnumerable<Doacoes> ListarPorRecebedor(string recebedor)
        {
            return _servDoar.ListarPorRecebedor(recebedor);
        }

        public IEnumerable<Doacoes> ListarDoacoesRealizadas(int cliente)
        {
            return _servDoar.ListarDoacoesRealizadas(cliente);
        }

        public IEnumerable<Doacoes> ListarDoacoesRecebidas(string cliente)
        {
            var logins = _repositorioRede.ListarPorLogin(cliente);
            string retorno = "";
            int i = 1;
            foreach (var item in logins)
            {
                if (i == 1)
                {
                    retorno = "'" + item.Apelido + "'";
                }
                else
                {
                    retorno = retorno + ", '" + item.Apelido + "'";
                }
                i++;
            }

            return _servDoar.ListarDoacoesRecebidas(retorno);
        }

        public decimal SomarDoacoes(int nivel, string apelido)
        {
            return _servDoar.SomarDoacoes(nivel, apelido);
        }

        public int ConfirmaRecebimento(int nivel, string cliente)
        {
            return _servDoar.ConfirmaRecebimento(nivel, cliente);
        }

        public void ExecutaDoacao(decimal valor, int nivel, int clienteId, string doador, string recebedor, int EntradaReentrada)
        {
            // Valor da doação
            var nivelDoacao = _repositorioNivel.BuscarPorNivel(nivel);
            var valorDoacao = (nivelDoacao.Entradas * nivelDoacao.ValorEntrada);
            var apelidoRecebedor = recebedor;
            var tipoEntrada = 0;

            if (nivel.Equals(1) && EntradaReentrada.Equals(0))
            {
                tipoEntrada = 0;
            }
            else
            {
                tipoEntrada = EntradaReentrada;
            }

            if (EntradaReentrada.Equals(2))
            {
                var nivelAcimao = (nivel - 1);

                if (nivelAcimao == 1)
                {
                    var retorno1 = _repositorioRede.BuscarPorApelido(recebedor).ApelidoRecebedor;
                    apelidoRecebedor = retorno1;
                }

                if (nivelAcimao == 2)
                {
                    var retorno1 = _repositorioRede.BuscarPorApelido(recebedor).ApelidoRecebedor;
                    var retorno2 = _repositorioRede.BuscarPorApelido(retorno1).ApelidoRecebedor;
                    apelidoRecebedor = retorno2;
                }

                if (nivelAcimao == 3)
                {
                    var retorno1 = _repositorioRede.BuscarPorApelido(recebedor).ApelidoRecebedor;
                    var retorno2 = _repositorioRede.BuscarPorApelido(retorno1).ApelidoRecebedor;
                    var retorno3 = _repositorioRede.BuscarPorApelido(retorno2).ApelidoRecebedor;
                    apelidoRecebedor = retorno3;
                }

                if (nivelAcimao == 4)
                {
                    var retorno1 = _repositorioRede.BuscarPorApelido(recebedor).ApelidoRecebedor;
                    var retorno2 = _repositorioRede.BuscarPorApelido(retorno1).ApelidoRecebedor;
                    var retorno3 = _repositorioRede.BuscarPorApelido(retorno2).ApelidoRecebedor;
                    var retorno4 = _repositorioRede.BuscarPorApelido(retorno3).ApelidoRecebedor;
                    apelidoRecebedor = retorno4;
                }
            }

            DateTime dataLimite = DateTime.Now.AddDays(1);
            var doacao = new Doacoes
            {
                MatrizId = 1,
                Nivel = nivel,
                ClienteId = clienteId,
                Doador = doador,
                Recebedor = apelidoRecebedor,
                ExpiraData = dataLimite,
                Tipo = tipoEntrada,
                Valor = valor,
                Status = 0
            };
            _servDoar.Add(doacao);
        }

        public Doacoes BuscarDoacaoPendente(int nivel, string doador, string recebedor, int status)
        {
            return _servDoar.BuscarDoacaoPendente(nivel, doador, recebedor, status);
        }

        public IEnumerable<Doacoes> BuscarEmAberto(int cliente)
        {
            return _servDoar.BuscarEmAberto(cliente);
        }

        public IEnumerable<Doacoes> ListarDoacoesEnviadas(string cliente)
        {
            return _servDoar.ListarDoacoesEnviadas(cliente);
        }

        public IEnumerable<Doacoes> BuscarPorDoadorPendente(string doador)
        {
            return _servDoar.BuscarPorDoadorPendente(doador);
        }

        public void AtualizaStatusDoador(int doador)
        {
            Clientes clientes = _repositorioCliente.GetById(doador);
            clientes.Status = 1;
            _repositorioCliente.Update(clientes);
        }

        public void AtualizaStatusPagamento(int nivel, string apelidoDoador, string apelidoRecebedor)
        {
            var doacao = _servDoar.BuscarDoacaoPendente(nivel, apelidoDoador, apelidoRecebedor, 1);
            doacao.ConfirmaDoacao = DateTime.Now;
            doacao.Status = 2;
            _servDoar.Update(doacao);
        }

        public void AtualizaStatusRecebedor(int recebedor)
        {
            Clientes novoStatus = _repositorioCliente.GetById(recebedor);
            novoStatus.Status = 0;
            _repositorioCliente.Update(novoStatus);
        }

        public bool GetComprovante(int cliente, string comprovante)
        {
            return _servDoar.GetComprovante(cliente, comprovante);
        }
    }
}
