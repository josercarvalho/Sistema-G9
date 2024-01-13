using SistemaG9.Domain.Interfaces.Services;

namespace SistemaG9.Aplication
{
    public class StartApp
    {
        private readonly IClienteService _cliente;
        private readonly IBancoService _banco;
        private readonly IDadosBancariosService _dadosbancarios;
        private readonly IRedeService _rede;
        private readonly IDoacaoService _doacao;
        private readonly IArquivoService _arquivo;
        private readonly INivelService _nivel;

        public StartApp(IClienteService servCliente,
                        IBancoService servBanco,
                        IDadosBancariosService servDadosBanco,
                        IRedeService servRede,
                        IDoacaoService servDoacao,
                        IArquivoService servArquivo,
                        INivelService servNivel)
        {
            _cliente = servCliente;
            _banco = servBanco;
            _dadosbancarios = servDadosBanco;
            _rede = servRede;
            _doacao = servDoacao;
            _arquivo = servArquivo;
            _nivel = servNivel;
        }


    }
}
