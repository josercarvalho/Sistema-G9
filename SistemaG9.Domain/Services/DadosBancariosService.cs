using System;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Domain.Services
{
    public class DadosBancariosService : ServiceBase<DadosBancarioViewModel>, IDadosBancariosService
    {
        private readonly IDadosBancariosRepository _dadosBancarios;
        public DadosBancariosService(IDadosBancariosRepository dadosBancarios)
            : base(dadosBancarios)
        {
            _dadosBancarios = dadosBancarios;
        }

        public DadosBancarioViewModel BuscarDadosBancariosRecebedor(int recebedor)
        {
            return _dadosBancarios.BuscarDadosBancariosRecebedor(recebedor);
        }

        public DadosBancarioViewModel BuscarPorClienteBanco(int cliente, int banco)
        {
            return _dadosBancarios.BuscarPorClienteBanco(cliente, banco);
        }

        public IEnumerable<DadosBancarioViewModel> ListarPorCliente(int cliente)
        {
            return _dadosBancarios.ListarPorCliente(cliente);
        }
    }
}
