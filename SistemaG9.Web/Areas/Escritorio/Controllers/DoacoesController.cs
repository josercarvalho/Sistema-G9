using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class DoacoesController : Controller
    {
        //private readonly EntitieContext _db = new EntitieContext();
        private readonly IDadosBancariosService _dadosBancarios;
        private readonly IDoacaoService _doacao;
        private readonly IBancoService _banco;
        private readonly IRedeService _rede;
        private readonly IClienteService _cliente;
        private readonly IArquivoService _arquivo;
        private readonly INivelService _nivel;

        public DoacoesController(IDoacaoService servDoacao,
                                        IBancoService servBanco,
                                        IRedeService servRede,
                                        IDadosBancariosService servDadosBancarios,
                                        IClienteService servCliente,
                                        IArquivoService servArquivo,
                                        INivelService servNivel)
        {
            _doacao = servDoacao;
            _banco = servBanco;
            _rede = servRede;
            _dadosBancarios = servDadosBancarios;
            _cliente = servCliente;
            _arquivo = servArquivo;
            _nivel = servNivel;
        }


        // GET: Escritorio/Doacoes
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var id = SessionManager.UsuarioLogado.ClienteId;

            if (SessionManager.UsuarioLogado.Status.Equals(0))
            {
                // Dados Bancários do Recebedor
                var recebdor = _rede.ListarPorCliente(id).FirstOrDefault();  // _db.Rede.FirstOrDefault(x => x.ClienteId.Equals(id));
                ViewBag.DadosBanco = _dadosBancarios.BuscarDadosBancariosRecebedor(recebdor.RecebedorId); // _db.DadoBancarios.Include(x => x.Cliente).Include(x => x.Banco).Where(x => x.ClienteId.Equals(recebdor.Recebedor));

                return RedirectToAction("HomeDoacao", "Home");
            }

            return View();
        }

        // GET: Escritorio/Doacoes
        public ActionResult Recebidas()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var login = SessionManager.UsuarioLogado.Login;

            var arquivo = _doacao.ListarDoacoesRecebidas(login);

            //ViewBag.Libera = arquivo.Count();

            //ViewBag.Arquivo = arquivo;

            return View(arquivo);
        }

        public ActionResult Realizadas()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var id = SessionManager.UsuarioLogado.ClienteId;
            var arquivo = _doacao.ListarDoacoesRealizadas(id);

            ViewBag.Arquivo = arquivo;

            return View(arquivo);
        }

        [HttpPost]
        public JsonResult LiberaPagamento(int nivel, string doador, string recebedor)
        {
            string mensagemErro = string.Empty;
            try
            {
                var doacao = _doacao.BuscarDoacaoPendente(nivel, doador, recebedor, 1);

                // Atualiza status de pagamento na tabela de doações como STATUS=2 (COMPROVADO)
                _doacao.AtualizaStatusPagamento(nivel, doador, recebedor);

                //Atualiza status dõ doador na Rede e do Cliente caso não haja nenhuma doação em aberto.
                _rede.AtualizaStatusDoador(doador);

                // Atualiza status do cliente doador
                _doacao.AtualizaStatusDoador(doacao.ClienteId);                

                TempData["SuccessMessage"] = "Doação confirmada com sucesso!";
                mensagemErro = "Doação confirmada com sucesso!";
            }
            catch (Exception ex)
            {
                mensagemErro = ex.Message;
            }

            return Json(mensagemErro, JsonRequestBehavior.AllowGet);
        }

        #region Entradas e Reentradas

        //[HttpPost]
        //public JsonResult Reentradas(int? id)
        //{
        //    var mensagem = "Reentradas efetuadas com sucesso!";
        //    try
        //    {
        //        if (id == null)
        //        {
        //            id = SessionManager.UsuarioLogado.ClienteId;
        //        }

        //        var login = SessionManager.UsuarioLogado.Login;
        //        var ultimasDoacoes =  _db.Doacao.OrderByDescending(x => x.ClienteId.Equals((object)login));
        //        var nivelAtual = ultimasDoacoes.FirstOrDefault().Nivel;
        //        var countDoacoes = ultimasDoacoes.Count(x => x.Nivel.Equals(nivelAtual) && x.Tipo.Equals(1));
        //        var doacoesDoNivel = _db.Niveis.FirstOrDefault(x => x.NivelMatriz.Equals(nivelAtual));
        //        if (countDoacoes == doacoesDoNivel.Entradas)
        //        {
        //            var reentradas = _db.Niveis.SingleOrDefault(x => x.NivelMatriz.Equals(nivelAtual + 1));
        //            CadastraEntradas(id, nivelAtual + 1, reentradas.Reentradas, 1, reentradas.ValorReentrada);
        //        }
        //        else
        //        {
        //            mensagem = "ATENÇÃO, você ainda não está ápto a fazer REENTRADAS!";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        mensagem = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        //    }

        //    return Json(mensagem, JsonRequestBehavior.DenyGet);
        //}

        //public void CadastraReentradas(int nivel, int cliente, string login, int quantidade)
        //{
        //    var apelido = "";
        //    var retornaPagamentos = _doacao.GetAll().Where(x => x.ClienteId.Equals(cliente) && x.Nivel.Equals(nivel) && x.Tipo.Equals(1));
        //    var doacoes = retornaPagamentos.Any() ? retornaPagamentos.Count() : 0;

        //    var valorDoacao = _nivel.BuscarPorNivel(1, 1);

        //    if (doacoes.Equals(valorDoacao.Entradas) == true)
        //    {
        //        for (int i = 1; i < quantidade; i++)
        //        {
        //            apelido = login + "-" + i;
        //            _rede.CadastoNaRede(cliente, 1, login, apelido);
        //        }
        //    }
        //}

        //private void RegistrarEntrada( int nivel, decimal valor)
        //{
        //    var ultimoCadastrado = _rede.UltimoDaRede(); // _db.Rede.Max(x => x.Posicao);
        //    // Busca cliente patrocinador da ultima posição
        //    var patrocinador = _rede.GetAll().FirstOrDefault(x => x.Posicao.Equals(ultimoCadastrado)).Recebedor; // _db.Rede.FirstOrDefault(x => x.Posicao.Equals(ultimoCadastrado)).Recebedor;
        //    // Busca quantidade do patrocinador na matriz que não pode exceder a 3
        //    var patrocinadorCount = _rede.GetAll().Count(x => x.Recebedor.Equals(patrocinador)); // _db.Rede.Count(x => x.Recebedor.Equals(patrocinador));
        //    // Determina o patrocinador para o próximo cliente da matriz
        //    //var origem = patrocinador.Count() < 3 ? origemDaPosicao : origemDaPosicao + 1;
        //    var apelido = "";
        //    if (nivel.Equals(1))
        //    {
        //        apelido = SessionManager.UsuarioLogado.Login;
        //    }
        //    else
        //    {
        //        apelido = SessionManager.UsuarioLogado.Login + "-" + nivel.ToString();
        //    }

        //    var origem = 0;
        //    if (patrocinadorCount < 3)
        //    {
        //        origem = patrocinador;
        //    }
        //    else
        //    {
        //        origem = patrocinador + 1;
        //    }

        //    var rede = new Rede();
        //    rede.MatrizId = matriz;
        //    rede.Posicao = ultimoCadastrado.Posicao + 1;
        //    rede.ClienteId = SessionManager.UsuarioLogado.ClienteId;
        //    rede.Nivel = nivel;
        //    rede.Login = SessionManager.UsuarioLogado.Login;
        //    rede.Apelido = apelido;
        //    rede.Recebedor = origem;
        //    rede.Status = 0;
        //    _rede.Add(rede);
        //}

        public ActionResult LiberaCadastro()
        {
            var id = SessionManager.UsuarioLogado.ClienteId;

            var arquivo = _arquivo.GetAll().Where(x => x.ClienteId.Equals(id));

            return View(arquivo);
        }

        #endregion
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _banco.Dispose();
                _cliente.Dispose();
                _dadosBancarios.Dispose();
                _doacao.Dispose();
                _rede.Dispose();
                _nivel.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AtualizaDoacao()
        {
            var Arquivo = _arquivo.GetAll().Where(x => x.Status.Equals("CONFIRMADO"));
            var doacao = new Doacoes();
            var rede = new Rede();

            foreach (var item in Arquivo)
            {
                rede = _rede.GetAll().Where(x => x.ClienteId.Equals(item.ClienteId) && x.RecebedorId.Equals(item.Recebedor)).FirstOrDefault();
                if (!rede.ClienteId.Equals(null))
                {
                    doacao = _doacao.GetAll().FirstOrDefault(x => x.Doador.Equals(rede.Apelido) && x.Recebedor.Equals(rede.ApelidoRecebedor));
                    doacao.Comprovante = item.Nome;
                    doacao.AnexoExtensao = item.AnexoExtensao;
                    doacao.DataEnvio = item.DataEnvio;
                    doacao.AnexoTipo = item.AnexoTipo;
                    doacao.Status = 2;
                    _doacao.Update(doacao);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}