using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using System.Linq;
using System;

namespace SistemaG9.Domain.Services
{
    public class RedeService : ServiceBase<Rede>, Interfaces.Services.IRedeService
    {
        private readonly Interfaces.Repositories.IRedeService _servRede;
        private readonly Interfaces.Services.IClienteService _servCliente;
        private readonly IDoacaoService _servDoacao;
        private readonly INivelService _servnivel;

        public RedeService(Interfaces.Repositories.IRedeService repository,
                            Interfaces.Services.IClienteService servCliente,
                            IDoacaoService serDoacao,
                            INivelService servNivel) : base(repository)
        {
            _servRede = repository;
            _servCliente = servCliente;
            _servDoacao = serDoacao;
            _servnivel = servNivel;
        }

        public Rede BuscarPorApelido( string apelido)
        {
            return _servRede.BuscarPorApelido(apelido);
        }

        public IEnumerable<Rede> ListarPorCliente( int cliente)
        {
            return _servRede.ListarPorCliente(cliente);
        }

        public IEnumerable<Rede> ListarPorLogin( string login)
        {
            return _servRede.ListarPorLogin(login);
        }

        public IEnumerable<Rede> ListarRede(int matriz)
        {
            return _servRede.ListarRede(matriz);
        }

        public Rede BuscarStatus( string apelido)
        {
            return _servRede.BuscarStatus(apelido);
        }

        public void CadastoNaRede(int id, int nivel, string login, string apelido)
        {
            // Busca ultima posição
            var clienteRecebedor = UltimoDaRede();
            var ultimoCadastrado = clienteRecebedor.Posicao;
            if (clienteRecebedor != null)
            {
                var recebedorDaPosicao = clienteRecebedor.RecebedorId;
                var apelidoRecebedor = clienteRecebedor.ApelidoRecebedor;
                // Busca quantidade do patrocinador na matriz que não pode exceder a 3
                var recebedor = _servRede.GetAll().Count(x => x.ApelidoRecebedor.Equals(apelidoRecebedor));
                // Determina o patrocinador para o próximo cliente da matriz
                int origem;
                if (recebedor < 3)
                {
                    origem = recebedorDaPosicao;
                }
                else
                {
                    origem = recebedorDaPosicao + 1;
                    apelidoRecebedor = BuscarApelido(clienteRecebedor.ApelidoRecebedor).Apelido;
                }

                var matriz = new Rede
                {
                    MatrizId = 1,
                    Nivel = nivel,
                    ClienteId = id,
                    Login = login,
                    Apelido = apelido,
                    RecebedorId = origem,
                    ApelidoRecebedor = apelidoRecebedor,
                    Posicao = ultimoCadastrado + 1,
                    Status = 0
                };

                _servRede.Add(matriz);

                decimal valorDoacao = 50;
                var tipoEntrada = 0;
                if (login != apelido) tipoEntrada = 1;

                _servDoacao.ExecutaDoacao(valorDoacao, nivel, id, apelido, apelidoRecebedor, tipoEntrada);
            }
        }

        public Rede UltimoDaRede()
        {
            return _servRede.UltimoDaRede();
        }

        public IEnumerable<Rede> ListarMinhaRede( int posicao)
        {
            return _servRede.ListarMinhaRede(posicao);
        }

        public IEnumerable<Rede> ListarRecebedores( string apelido)
        {
            return _servRede.ListarRecebedores(apelido);
        }

        public Rede BuscarApelido( string apelidoRecebedor)
        {
            return _servRede.BuscarApelido(apelidoRecebedor);
        }

        public void AtualizaNivelRede( int nivel, string doador)
        {
            Rede novoNivel = _servRede.BuscarPorApelido(doador);
            novoNivel.Nivel = nivel + 1;
            novoNivel.Status = 0;
            _servRede.Update(novoNivel);
        }

        public IEnumerable<Rede> ListarDoacoesPendentes()
        {
            return _servRede.GetAll().Where(x => x.Status.Equals(0));
        }

        public void AtualizaStatusDoador( string doador)
        {
            Rede rede = _servRede.BuscarPorApelido(doador);
            rede.Status = 1;
            _servRede.Update(rede);

            // Busca na rede se possui mais algum status=0 para atualizar tabela de Cliente.
            var redePorCliente = _servRede.GetAll().Where(x => x.ClienteId.Equals(rede.ClienteId) && x.Status.Equals(0));
            if (redePorCliente.Count() == 0)
            {
                Clientes cliente = _servCliente.GetById(rede.ClienteId);
                cliente.Status = 1;
                _servCliente.Update(cliente);
            }
        }
    }
}
