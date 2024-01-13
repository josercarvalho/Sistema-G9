using SistemaG9.Domain.Models;
using System;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Interfaces.Aplication;

namespace SistemaG9.Aplication.Services
{
    public class ClienteApp : AppServiceBase<Clientes>, IClienteApp
    {
        private readonly IClienteService _clienteApp;
        public ClienteApp(IClienteService appBase) : base(appBase)
        {
            _clienteApp = appBase;
        }

        public void ComprovanteEnviado(int id)
        {
            throw new NotImplementedException();
        }
    }
}
