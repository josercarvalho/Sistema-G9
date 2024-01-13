using System;
using System.Collections.Generic;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;

namespace SistemaG9.Domain.Services
{
    public class ArquivoService :ServiceBase<Arquivos>, IArquivoService
    {
        private readonly IArquivoRepository _arquivoRepository;

        public ArquivoService(IRepositorioBase<Arquivos> repository, IArquivoRepository arquivoRepository) : base(repository)
        {
            _arquivoRepository = arquivoRepository;
        }

        public void AtualizaComprovante(int doador, int recebedor)
        {
            Arquivos confirmarArquivo = _arquivoRepository.BuscarPendente(doador, recebedor);
            confirmarArquivo.Status = "CONFIRMADO";
            _arquivoRepository.Update(confirmarArquivo);
        }

        public Arquivos BuscarPendente(int doador, int recebedor)
        {
            return _arquivoRepository.BuscarPendente(doador, recebedor);
        }

        public IEnumerable<Arquivos> ListarPorDoador(int clienteId)
        {
            return _arquivoRepository.ListarPorDoador(clienteId);
        }

        public IEnumerable<Arquivos> ListarPorRecebedor(int recebedor)
        {
            return _arquivoRepository.ListarPorRecebedor(recebedor);
        }

        public IEnumerable<Arquivos> ListarPorStatus(int clienteId, string status)
        {
            return _arquivoRepository.ListarPorStatus(clienteId, status);
        }
    }
}
