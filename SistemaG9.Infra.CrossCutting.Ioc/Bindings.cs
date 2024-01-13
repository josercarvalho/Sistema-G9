using SimpleInjector;
using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Services;
using SistemaG9.Infra.Data.Repositories;

namespace SistemaG9.Infra.CrossCutting.Ioc
{
    public class Bindings
    {
        /// <summary>
        /// Install-Package SimpleInjector
        /// Install-Package CommonServiceLocator -Version 1.3.0
        /// Install-Package CommonServiceLocator.SimpleInjectorAdapter
        /// </summary>
        public static void Start(Container container)
        {
            //Infrastrutura Repositorio
            container.Register(typeof(IRepositorioBase<>), typeof(RepositorioBase<>), Lifestyle.Scoped);
            container.Register(typeof(IArquivoRepository), typeof(ArquivoRepository), Lifestyle.Scoped);
            container.Register(typeof(IBancoRepository), typeof(BancoRepository), Lifestyle.Scoped);
            container.Register(typeof(ICidadeRepository), typeof(CidadeRepository), Lifestyle.Scoped);
            container.Register(typeof(IClienteRepository), typeof(ClienteRepository), Lifestyle.Scoped);
            container.Register(typeof(IDadosBancariosRepository), typeof(DadosBancariosRepository), Lifestyle.Scoped);
            container.Register(typeof(IDoacoesRepository), typeof(DoacoesRepository), Lifestyle.Scoped);
            container.Register(typeof(IEstadoRepository), typeof(EstadoRepository), Lifestyle.Scoped);
            container.Register(typeof(IMatrizRepositorio), typeof(MatrizRepository), Lifestyle.Scoped);
            container.Register(typeof(INivelRepository), typeof(NivelRepository), Lifestyle.Scoped);
            container.Register(typeof(IPerfilUsuarioRepository), typeof(PerfilUsuarioRepository), Lifestyle.Scoped);
            container.Register(typeof(Domain.Interfaces.Repositories.IRedeService), typeof(RedeRepository), Lifestyle.Scoped);
            
            //Serviços de Dominio
            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>), Lifestyle.Scoped);
            container.Register(typeof(IArquivoService), typeof(ArquivoService), Lifestyle.Scoped);
            container.Register(typeof(IBancoService), typeof(BancosService), Lifestyle.Scoped);
            container.Register(typeof(ICidadeService), typeof(CidadeService), Lifestyle.Scoped);
            container.Register(typeof(Domain.Interfaces.Services.IClienteService), typeof(Domain.Services.IClienteService), Lifestyle.Scoped);
            container.Register(typeof(IDadosBancariosService), typeof(DadosBancariosService), Lifestyle.Scoped);
            container.Register(typeof(IDoacaoService), typeof(DoacaoService), Lifestyle.Scoped);
            container.Register(typeof(IEstadoService), typeof(EstadoService), Lifestyle.Scoped);
            container.Register(typeof(IMatrizService), typeof(MatrizService), Lifestyle.Scoped);
            container.Register(typeof(INivelService), typeof(NivelService), Lifestyle.Scoped);
            container.Register(typeof(IPerfilUsuarioService), typeof(PerfilUsuarioService), Lifestyle.Scoped);
            container.Register(typeof(Domain.Interfaces.Services.IRedeService), typeof(RedeService), Lifestyle.Scoped);

            //Serviços de Aplicacao
            //container.Register(typeof(IAppServiceBase<>), typeof(AppServiceBase<>), Lifestyle.Scoped);
            //container.Register(typeof(IClienteApp<>), typeof(ClienteApp<>), Lifestyle.Scoped);


            //Service Locator
            //ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocatorAdapter(container));
        }
    }
}
