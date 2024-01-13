using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using SistemaG9.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteService _cliente;
        private readonly IBancoService _banco;
        private readonly IDadosBancariosService _dadosbancarios;
        private readonly IRedeService _rede;
        private readonly IDoacaoService _doacao;
        private readonly IArquivoService _arquivo;
        private readonly INivelService _nivel;

        public HomeController(IClienteService servCliente,
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

        /// <summary>
        /// The home page of the AdminLTE demo dashboard, recreated in this new system
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var id = SessionManager.UsuarioLogado.ClienteId;
            var login = SessionManager.UsuarioLogado.Login;
            var status = SessionManager.UsuarioLogado.Status;

            #region Administrarivo Área

            var perfilUser = SessionManager.UsuarioLogado.PerfilUsuarioId;

            if (perfilUser.Equals(1)) return View();

            #endregion

            #region Comprovantes

            //Checar se existem doações a serem realizadas antes de quaisquer procedimento tormando usuário Habilitado ou não
            var comprovanteEnviado = _doacao.GetAll().Where(x => x.ClienteId.Equals(id) && (!x.Status.Equals(2))).Count();
            if (comprovanteEnviado > 0) return RedirectToAction("HomeDoacao", "Home");

            // Seleciona todas os recebimentos de COMPROVANTES ENVIADOS PENDENTES do Clientes
            var comprovante = _doacao.ListarDoacoesEnviadas(login);
            if (!comprovante.Count().Equals(0))
            {
                ViewBag.CaminhoFile = "/uploads/";
                ViewBag.Comprovantes = comprovante;
                return View();
            }

            #endregion

            #region Reentradas

            //Verifica recebimentos para realização das reentradas por nível
            //var rede = _rede.BuscarPorApelido(1, login);
            var clienteId = SessionManager.UsuarioNaRede.ClienteId;
            var clienteRecebedorId = SessionManager.UsuarioNaRede.RecebedorId;
            var nivelAtual = SessionManager.UsuarioNaRede.Nivel;
            var apelido = SessionManager.UsuarioNaRede.Apelido;
            var apelidoRecebedor = SessionManager.UsuarioNaRede.ApelidoRecebedor;
            var recebedorCount = _rede.ListarPorLogin(login).Count();
            var doacao = new Doacoes();
            if (apelido.Equals(login))
            {
                doacao = _doacao.BuscarDoacaoPendente(nivelAtual, apelido, apelidoRecebedor, 1);
                if (doacao != null)
                {
                    return RedirectToAction("HomeDoacao", "Home");
                }

                //Verifica Doações para realização de Reentradas automáticas.                
                var docacoesRecebidas = _doacao.ConfirmaRecebimento(nivelAtual, apelido);
                var quantidadeDeDoacoes = _nivel.BuscarPorNivel(nivelAtual);
                var entradas = quantidadeDeDoacoes.Entradas;
                var reentradas = quantidadeDeDoacoes.Reentradas;
                //var recebedorNovo = new Rede();

                if (docacoesRecebidas == entradas)
                {
                    // Executa REENTRADA com as devidas doações de 50.00
                    //var recebedorID = _rede.BuscarPorApelido(1, doacao.Recebedor).ClienteId;
                    string apelidoNovo = string.Empty;
                    for (int i = 1; i <= reentradas; i++)
                    {
                        apelidoNovo = apelido + "-" + recebedorCount;
                        _rede.CadastoNaRede(clienteId, 1, apelido, apelidoNovo);

                        recebedorCount++;
                    }

                    // Atualiza status do Cliente 
                    _cliente.AtualizaStatus(1, clienteId);

                    // Atualiza novo nível para o Doador na Rede após a reentrada
                    _rede.AtualizaNivelRede(nivelAtual, apelido);

                    // Atualiza status do cliente Recebedor
                    _doacao.AtualizaStatusRecebedor(clienteRecebedorId);

                    SessionManager.UsuarioLogado.Status = 0;

                    //return RedirectToAction("HomeDoacao", "Home");
                }
            }

            #endregion

            #region Doações            

            //Verifica recebimentos para executar DOAÇÃO mediante Apelidos da Rede
            var logins = _rede.GetAll().Where(x => x.ClienteId.Equals(id)).ToList();
            Nivel nivel = new Nivel();
            int totalRecebido;
            int recebidas;
            int doacaoJaEnviada;
            int nivelApelido;
            foreach (var item in logins)
            {
                nivelApelido = item.Nivel;
                if (item.Login.Equals(item.Apelido) && (!item.Nivel.Equals(1))) nivelApelido = item.Nivel - 1;

                totalRecebido = _doacao.ConfirmaRecebimento((nivelApelido), item.Apelido);
                nivel = _nivel.BuscarPorNivel(nivelApelido);
                recebidas = nivel.Entradas;
                comprovanteEnviado = _doacao.GetAll().Where(x => x.Doador.Equals(item.Apelido) && x.Nivel.Equals(item.Nivel) && x.Status.Equals(1)).Count();
                if ((totalRecebido == recebidas) && comprovanteEnviado == 0)
                {
                    doacaoJaEnviada = _doacao.GetAll().Where(x => x.Doador.Equals(item.Apelido) && x.Nivel.Equals(item.Nivel)).Count();
                    if (doacaoJaEnviada.Equals(0))
                    {
                        var valorDoar = _nivel.BuscarPorNivel(item.Nivel).ValorEntrada;
                        _doacao.ExecutaDoacao(valorDoar, item.Nivel, item.ClienteId, item.Apelido, item.ApelidoRecebedor, 2);
                        SessionManager.UsuarioLogado.Status = 0;
                    }
                }
            }

            #endregion

            if (SessionManager.UsuarioLogado.Status.Equals(0)) return RedirectToAction("HomeDoacao", "Home");

            return View();
        }

        #region Doações, Envio e Liberações de Comprovantes

        public ActionResult HomeDoacao()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var id = SessionManager.UsuarioLogado.ClienteId;
            var apelido = SessionManager.UsuarioLogado.Login;

            //TempData["SuccessMessage"] = null;

            // 1º Passo: verificar se recebedor está ATIVO a receber doações
            var recebedorSeAtivo = _rede.ListarRecebedores(apelido);

            if (recebedorSeAtivo.Count() > 0)
            {
                foreach (var item in recebedorSeAtivo)
                {
                    if (item.Status.Equals(0))
                    {
                        var recebedorAtivo = _doacao.BuscarEmAberto(item.RecebedorId);
                        if (recebedorAtivo.Count() > 0) return RedirectToAction("Bloqueado", "Home");
                    }
                }
            }

            // 2º Passo: verificar se existe pandencia (status = 0 ) como Doador na DOACÃO 
            var statusDoacao = (_doacao.BuscarPorDoadorPendente(apelido)).ToList();
            if (statusDoacao.Count() > 0) // Se existir veirificar se foi realizado Envio de Comprovante
            {
                //var comprovante = statusDoacao.Count(x=>x.Status.Equals(0)); // _doacao.GetAll().Where(x => x.ClienteId.Equals(id) && x.Status.Equals(1)).Count();
                if (statusDoacao.Count(x => x.Status.Equals(0)) > 0)
                {
                    ViewBag.ArquivoEnviado = false;
                }
                else if (statusDoacao.Count(x => x.Status.Equals(1)) > 0)
                {
                    ViewBag.ArquivoEnviado = true;
                    TempData["SuccessMessage"] = "Aguardando liberação do seu pagamento. Tente mais tarde!";
                }
            }
            else
            {
                ViewBag.ArquivoEnviado = false;
            }

            ViewBag.Arquivo = statusDoacao.Count();

            // Seleciona todas as doações a receber do Clientes
            var recebimentos = _doacao.BuscarEmAberto(id);
            IList<ContatosViewModel> dadosContato = new List<ContatosViewModel>();

            if (recebimentos.Count() > 0)
            {
                var recebedor = new Rede();
                var banco = new Clientes();
                var dadosBanco = new List<BancosDoContatoViewModel>();
                var dados = new ContatosViewModel();
                var i = 0;

                foreach (var item in recebimentos)
                {
                    if (item.Status.Equals(0))
                    {
                        recebedor = _rede.BuscarPorApelido(item.Recebedor);
                        banco = _cliente.GetById(recebedor.ClienteId);
                        i++;
                        dados = new ContatosViewModel();
                        dados.ID = i;
                        dados.Telefone = recebedor.Cliente.Telefone;
                        dados.WhatsApp = recebedor.Cliente.WhatsApp;
                        dados.Valor = item.Valor;
                        dados.ClienteId = item.ClienteId;
                        dados.Nivel = item.Nivel;
                        dados.Tipo = item.Tipo;
                        dados.Doador = item.Doador;
                        dados.Recebedor = item.Recebedor;
                        dados.dadosBancarios = _dadosbancarios.GetAll().Where(x => x.ClienteId.Equals(banco.ClienteId));

                        foreach (var campo in dados.dadosBancarios)
                        {
                            var novoRegistro = new BancosDoContatoViewModel
                            {
                                ID = i,
                                ClienteId = campo.ClienteId,
                                Login = dados.Recebedor,
                                Banco = campo.Banco.Nome,
                                BancoId = campo.BancoId
                            };
                            dadosBanco.Add(novoRegistro);
                        }

                        dadosContato.Add(dados);
                    }
                    ViewBag.DadosConta = dadosBanco;
                }
            }

            //TempData["warning"] = "Mensagem de warning!!";
            //TempData["success"] = "Mensagem de sucesso!!";
            //TempData["info"] = "Mensagem de informação!!";
            //TempData["error"] = "Mensagem de erro!!";

            return View(dadosContato);
        }

        public JsonResult GetConta(int id, int cb)
        {

            string mensagemErro = string.Empty;
            DadosBancarioViewModel cliente = null;

            try
            {
                cliente = _dadosbancarios.BuscarPorClienteBanco(id, cb);

                if (cliente == null)
                {
                    throw new Exception("Contato não localizado!");
                    //mensagemErro = "Contato não localizado!";
                }

            }
            catch (Exception ex)
            {

                mensagemErro = ex.Message;

            }

            return Json(
                new
                {
                    erro = mensagemErro,
                    dados = cliente != null ? new
                    {
                        id = cliente.ClienteId,
                        banco = cliente.Banco.Nome,
                        titular = cliente.Titular,
                        agencia = cliente.Agencia,
                        conta = cliente.Conta,
                        operacao = cliente.Operacao,
                        tipo = cliente.TipoConta == 0 ? "POUPANCA" : "CONTA CORRENTE"
                    } : null,
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult Bloqueado()
        {
            if (SessionManager.UsuarioNaRede.Nivel == 1)
            {
                TempData["SuccessMessage"] = "Estamos processando seus dados e sorteando quem deverá receber sua doação. Tente mais tarde.";
            }
            else
            {
                TempData["SuccessMessage"] = "Aguarde a regularização das pendências do recebedor de sua doação. Por favor tente mais tarde!";
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EnviarArquivo(Picture picture, FormCollection form)
        {
            string mensagemErro = string.Empty;

            if (picture.File == null)
            {
                TempData["FileErroMessage"] = "Erro ao tentar enviar o arquivo. Avise o SUPORTE!";
                //ModelState.AddModelError("Comprovante", "Erro ao tentar enviar o arquivo. Avise o SUPORTE!");
                return RedirectToAction("HomeDoacao", "Home");
            }

            //if (form["Comprovante"] == null)
            //{
            //    ModelState.AddModelError("Comprovante","Erro ao tentar enviar o arquivo. Avise o SUPORTE!");
            //    return RedirectToAction("HomeDoacao", "Home");
            //}

            if (Request.Files.Count != 1)
            {
                //ModelState.AddModelError("Comprovante", "Nenhum arquivo foi selecionado. Tente novamente!");
                //return RedirectToAction("HomeDoacao", "Home");

                mensagemErro = "Nenhum arquivo foi selecionado. Tente novamente!";
                return Json(mensagemErro, JsonRequestBehavior.AllowGet);
            }

            var post = Request.Files[0];

            if (post == null)
            {
                TempData["FileErroMessage"] = "Nenhum arquivo foi selecionado. Tente novamente!";
                return RedirectToAction("HomeDoacao", "Home");

                //mensagemErro = "Nenhum arquivo foi selecionado. Tente novamente!";
                //return Json(mensagemErro, JsonRequestBehavior.AllowGet);
            }

            string extensao = Path.GetExtension(post.FileName);
            string[] extensoesValidas = new string[] { ".jpg", ".png", ".pdf", ".jpeg" };

            if (!extensoesValidas.Contains(extensao))
            {
                TempData["FileErroMessage"] = string.Format("Extensão de arquivo *{0} não suportada", extensao);
                return RedirectToAction("HomeDoacao", "Home");

                //mensagemErro = string.Format("Extensão de arquivo *{0} não suportada", extensao);
                //return Json(mensagemErro, JsonRequestBehavior.AllowGet);
            }

            var encontraArquivo = _doacao.GetComprovante(SessionManager.UsuarioLogado.ClienteId, post.FileName);
            if (encontraArquivo)
            {
                TempData["FileErroMessage"] = "Este arquivo já foi enviado. Tente outro!";
                return RedirectToAction("HomeDoacao", "Home");

                //mensagemErro = "Este arquivo já foi enviado. Tente outro!";
                //return Json(mensagemErro, JsonRequestBehavior.AllowGet);
            }

            try
            {
                var clienteId = form["ClienteId"];
                var doador = form["Doador"];
                var nivel = Convert.ToInt32(form["Nivel"]);
                var tipo = form["Tipo"];
                var Recebedor = form["Recebedor"];

                var file = new FileInfo(post.FileName);
                var filename = Path.GetFileName(picture.File.FileName);
                var path = Path.Combine(Server.MapPath("~/uploads"), filename);

                picture.File.SaveAs(path);

                var obj = _doacao.BuscarDoacaoPendente(nivel, doador, Recebedor, 0);
                obj.Comprovante = filename; // file.Name;
                obj.AnexoExtensao = file.Extension;
                obj.AnexoTipo = post.ContentType;
                obj.DataEnvio = DateTime.Now;
                obj.Status = 1;
                _doacao.Update(obj);

                var clienteRec = form["Nome"]; // _cliente.GetById(recebedorId);
                TempData["SuccessMessage"] = "Arquivo Enviado com Sucesso para " + clienteRec.ToUpper() + ". Aguardando liberação do seu pagamento. Tente mais tarde!";
                //mensagemErro = "Envio realizado com sucesso!";
            }
            catch (Exception ex)
            {
                //mensagemErro = ex.Message;
                TempData["FileErroMessage"] = "Erro ao tentar enviar o arquivo. Avise o SUPORTE!";
                return RedirectToAction("HomeDoacao", "Home");
            }

            ViewBag.ArquivoEnviado = true;

            //return Json(mensagemErro, JsonRequestBehavior.AllowGet);

            return RedirectToAction("HomeDoacao", "Home");
        }

        #endregion

        public ActionResult Construcao()
        {
            ViewBag.Message = "Breve em funcionamento.";

            return View();
        }

    }
}