using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using SistemaG9.Web.ViewModels;
using System.Linq;
using System.Web.Mvc;
using System.Collections.Generic;
using System;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class RedeController : Controller
    {
        private readonly IRedeService _repoRede;
        private readonly IClienteService _cliente;
        private readonly IDoacaoService _doacao;

        public RedeController(IClienteService servCliente, IDoacaoService servDoacao, IRedeService repoRede)
        {
            _repoRede = repoRede;
            _cliente = servCliente;
            _doacao = servDoacao;
        }

        // GET: Escritorio/Rede
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            return View(_repoRede.GetAll());

        }

        public ActionResult Matriz()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var rede = _repoRede.ListarRede(1);

            return View(rede);
        }

        public JsonResult GetContato(int id)
        {

            string mensagemErro = string.Empty;
            Clientes cliente = null;

            try
            {
                cliente = _cliente.GetById(id);

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
                        login = cliente.Login,
                        nome = cliente.Nome,
                        telefone = cliente.Telefone,
                        operadora = cliente.Operadora,
                        whatsapp = cliente.WhatsApp,
                        email = cliente.Email,
                        skype = cliente.SKYPE
                    } : null,
                },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult RedeGrafica(string apelido)
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            if (apelido == null)
            {
                apelido = SessionManager.UsuarioNaRede.Apelido;
            }

            RedeViewModel rede = new RedeViewModel();
            //var novaRede = new List<Doacoes>();
            var novaRede = _doacao.ListarPorRecebedor(apelido);
            //var novaRede = _doacao.GetAll().Where(x => x.Recebedor.Equals(apelido)).OrderBy(x => new { x.Nivel, x.ClienteId });

            rede = CarregaRede(novaRede);
            rede.Posicao_Apelido_0 = apelido;
            rede.Posicao_ID_0 = SessionManager.UsuarioLogado.ClienteId;
            rede.Posicao_Status_0 = 1;

            ViewBag.apelido = new SelectList( _repoRede.ListarPorLogin(SessionManager.UsuarioLogado.Login),"apelido", "Apelido");

            return View(rede);
        }

        private RedeViewModel CarregaRede(IEnumerable<Doacoes> redeGrafica)
        {
            var rede = new RedeViewModel();
            var ii = 1;
            foreach (var item in redeGrafica)
            {
                if (ii.Equals(1))
                {
                    rede.Posicao_ID_1 = item.ClienteId;
                    rede.Posicao_Apelido_1 = item.Doador;
                    rede.Posicao_Status_1 = (int)item.Status;
                }
                if (ii.Equals(2))
                {
                    rede.Posicao_ID_2 = item.ClienteId;
                    rede.Posicao_Apelido_2 = item.Doador;
                    rede.Posicao_Status_2 = (int)item.Status;
                }
                if (ii.Equals(3))
                {
                    rede.Posicao_ID_3 = item.ClienteId;
                    rede.Posicao_Apelido_3 = item.Doador;
                    rede.Posicao_Status_3 = (int)item.Status;
                }

                if (ii.Equals(4))
                {
                    rede.Posicao_ID_4 = item.ClienteId;
                    rede.Posicao_Apelido_4 = item.Doador;
                    rede.Posicao_Status_4 = (int)item.Status;
                }
                if (ii.Equals(5))
                {
                    rede.Posicao_ID_5 = item.ClienteId;
                    rede.Posicao_Apelido_5 = item.Doador;
                    rede.Posicao_Status_5 = (int)item.Status;
                }
                if (ii.Equals(6))
                {
                    rede.Posicao_ID_6 = item.ClienteId;
                    rede.Posicao_Apelido_6 = item.Doador;
                    rede.Posicao_Status_6 = (int)item.Status;
                }
                if (ii.Equals(7))
                {
                    rede.Posicao_ID_7 = item.ClienteId;
                    rede.Posicao_Apelido_7 = item.Doador;
                    rede.Posicao_Status_7 = (int)item.Status;
                }
                if (ii.Equals(8))
                {
                    rede.Posicao_ID_8 = item.ClienteId;
                    rede.Posicao_Apelido_8 = item.Doador;
                    rede.Posicao_Status_8 = (int)item.Status;
                }
                if (ii.Equals(9))
                {
                    rede.Posicao_ID_9 = item.ClienteId;
                    rede.Posicao_Apelido_9 = item.Doador;
                    rede.Posicao_Status_9 = (int)item.Status;
                }
                if (ii.Equals(10))
                {
                    rede.Posicao_ID_10 = item.ClienteId;
                    rede.Posicao_Apelido_10 = item.Doador;
                    rede.Posicao_Status_10 = (int)item.Status;
                }
                if (ii.Equals(11))
                {
                    rede.Posicao_ID_11 = item.ClienteId;
                    rede.Posicao_Apelido_11 = item.Doador;
                    rede.Posicao_Status_11 = (int)item.Status;
                }
                if (ii.Equals(12))
                {
                    rede.Posicao_ID_12 = item.ClienteId;
                    rede.Posicao_Apelido_12 = item.Doador;
                    rede.Posicao_Status_12 = (int)item.Status;
                }

                if (ii.Equals(13))
                {
                    rede.Posicao_ID_13 = item.ClienteId;
                    rede.Posicao_Apelido_13 = item.Doador;
                    rede.Posicao_Status_13 = (int)item.Status;
                }
                if (ii.Equals(14))
                {
                    rede.Posicao_ID_14 = item.ClienteId;
                    rede.Posicao_Apelido_14 = item.Doador;
                    rede.Posicao_Status_14 = (int)item.Status;
                }
                if (ii.Equals(15))
                {
                    rede.Posicao_ID_15 = item.ClienteId;
                    rede.Posicao_Apelido_15 = item.Doador;
                    rede.Posicao_Status_15 = (int)item.Status;
                }
                if (ii.Equals(16))
                {
                    rede.Posicao_ID_16 = item.ClienteId;
                    rede.Posicao_Apelido_16 = item.Doador;
                    rede.Posicao_Status_16 = (int)item.Status;
                }
                if (ii.Equals(17))
                {
                    rede.Posicao_ID_17 = item.ClienteId;
                    rede.Posicao_Apelido_17 = item.Doador;
                    rede.Posicao_Status_17 = (int)item.Status;
                }
                if (ii.Equals(18))
                {
                    rede.Posicao_ID_18 = item.ClienteId;
                    rede.Posicao_Apelido_18 = item.Doador;
                    rede.Posicao_Status_18 = (int)item.Status;
                }
                if (ii.Equals(19))
                {
                    rede.Posicao_ID_19 = item.ClienteId;
                    rede.Posicao_Apelido_19 = item.Doador;
                    rede.Posicao_Status_19 = (int)item.Status;
                }
                if (ii.Equals(20))
                {
                    rede.Posicao_ID_20 = item.ClienteId;
                    rede.Posicao_Apelido_20 = item.Doador;
                    rede.Posicao_Status_20 = (int)item.Status;
                }
                if (ii.Equals(21))
                {
                    rede.Posicao_ID_21 = item.ClienteId;
                    rede.Posicao_Apelido_21 = item.Doador;
                    rede.Posicao_Status_21 = (int)item.Status;
                }
                if (ii.Equals(22))
                {
                    rede.Posicao_ID_22 = item.ClienteId;
                    rede.Posicao_Apelido_22 = item.Doador;
                    rede.Posicao_Status_22 = (int)item.Status;
                }
                if (ii.Equals(23))
                {
                    rede.Posicao_ID_23 = item.ClienteId;
                    rede.Posicao_Apelido_23 = item.Doador;
                    rede.Posicao_Status_23 = (int)item.Status;
                }
                if (ii.Equals(24))
                {
                    rede.Posicao_ID_24 = item.ClienteId;
                    rede.Posicao_Apelido_24 = item.Doador;
                    rede.Posicao_Status_24 = (int)item.Status;
                }
                if (ii.Equals(25))
                {
                    rede.Posicao_ID_25 = item.ClienteId;
                    rede.Posicao_Apelido_25 = item.Doador;
                    rede.Posicao_Status_25 = (int)item.Status;
                }
                if (ii.Equals(26))
                {
                    rede.Posicao_ID_26 = item.ClienteId;
                    rede.Posicao_Apelido_26 = item.Doador;
                    rede.Posicao_Status_26 = (int)item.Status;
                }
                if (ii.Equals(27))
                {
                    rede.Posicao_ID_27 = item.ClienteId;
                    rede.Posicao_Apelido_27 = item.Doador;
                    rede.Posicao_Status_27 = (int)item.Status;
                }
                if (ii.Equals(28))
                {
                    rede.Posicao_ID_28 = item.ClienteId;
                    rede.Posicao_Apelido_28 = item.Doador;
                    rede.Posicao_Status_28 = (int)item.Status;
                }
                if (ii.Equals(29))
                {
                    rede.Posicao_ID_29 = item.ClienteId;
                    rede.Posicao_Apelido_29 = item.Doador;
                    rede.Posicao_Status_29 = (int)item.Status;
                }
                if (ii.Equals(30))
                {
                    rede.Posicao_ID_30 = item.ClienteId;
                    rede.Posicao_Apelido_30 = item.Doador;
                    rede.Posicao_Status_30 = (int)item.Status;
                }
                if (ii.Equals(31))
                {
                    rede.Posicao_ID_31 = item.ClienteId;
                    rede.Posicao_Apelido_31 = item.Doador;
                    rede.Posicao_Status_31 = (int)item.Status;
                }
                if (ii.Equals(32))
                {
                    rede.Posicao_ID_32 = item.ClienteId;
                    rede.Posicao_Apelido_32 = item.Doador;
                    rede.Posicao_Status_32 = (int)item.Status;
                }
                if (ii.Equals(33))
                {
                    rede.Posicao_ID_33 = item.ClienteId;
                    rede.Posicao_Apelido_33 = item.Doador;
                    rede.Posicao_Status_33 = (int)item.Status;
                }
                if (ii.Equals(34))
                {
                    rede.Posicao_ID_34 = item.ClienteId;
                    rede.Posicao_Apelido_34 = item.Doador;
                    rede.Posicao_Status_34 = (int)item.Status;
                }
                if (ii.Equals(35))
                {
                    rede.Posicao_ID_35 = item.ClienteId;
                    rede.Posicao_Apelido_35 = item.Doador;
                    rede.Posicao_Status_35 = (int)item.Status;
                }
                if (ii.Equals(36))
                {
                    rede.Posicao_ID_36 = item.ClienteId;
                    rede.Posicao_Apelido_36 = item.Doador;
                    rede.Posicao_Status_36 = (int)item.Status;
                }
                if (ii.Equals(37))
                {
                    rede.Posicao_ID_37 = item.ClienteId;
                    rede.Posicao_Apelido_37 = item.Doador;
                    rede.Posicao_Status_37 = (int)item.Status;
                }
                if (ii.Equals(38))
                {
                    rede.Posicao_ID_38 = item.ClienteId;
                    rede.Posicao_Apelido_38 = item.Doador;
                    rede.Posicao_Status_38 = (int)item.Status;
                }
                if (ii.Equals(39))
                {
                    rede.Posicao_ID_39 = item.ClienteId;
                    rede.Posicao_Apelido_39 = item.Doador;
                    rede.Posicao_Status_39 = (int)item.Status;
                }
                if (ii.Equals(40))
                {
                    rede.Posicao_ID_40 = item.ClienteId;
                    rede.Posicao_Apelido_40 = item.Doador;
                    rede.Posicao_Status_40 = (int)item.Status;
                }

                if (ii.Equals(41))
                {
                    rede.Posicao_ID_41 = item.ClienteId;
                    rede.Posicao_Apelido_41 = item.Doador;
                    rede.Posicao_Status_41 = (int)item.Status;
                }
                if (ii.Equals(42))
                {
                    rede.Posicao_ID_42 = item.ClienteId;
                    rede.Posicao_Apelido_42 = item.Doador;
                    rede.Posicao_Status_42 = (int)item.Status;
                }
                if (ii.Equals(43))
                {
                    rede.Posicao_ID_43 = item.ClienteId;
                    rede.Posicao_Apelido_43 = item.Doador;
                    rede.Posicao_Status_43 = (int)item.Status;
                }
                if (ii.Equals(44))
                {
                    rede.Posicao_ID_44 = item.ClienteId;
                    rede.Posicao_Apelido_44 = item.Doador;
                    rede.Posicao_Status_44 = (int)item.Status;
                }
                if (ii.Equals(45))
                {
                    rede.Posicao_ID_45 = item.ClienteId;
                    rede.Posicao_Apelido_45 = item.Doador;
                    rede.Posicao_Status_45 = (int)item.Status;
                }
                if (ii.Equals(46))
                {
                    rede.Posicao_ID_46 = item.ClienteId;
                    rede.Posicao_Apelido_46 = item.Doador;
                    rede.Posicao_Status_46 = (int)item.Status;
                }
                if (ii.Equals(47))
                {
                    rede.Posicao_ID_47 = item.ClienteId;
                    rede.Posicao_Apelido_47 = item.Doador;
                    rede.Posicao_Status_47 = (int)item.Status;
                }
                if (ii.Equals(48))
                {
                    rede.Posicao_ID_48 = item.ClienteId;
                    rede.Posicao_Apelido_48 = item.Doador;
                    rede.Posicao_Status_48 = (int)item.Status;
                }
                if (ii.Equals(49))
                {
                    rede.Posicao_ID_49 = item.ClienteId;
                    rede.Posicao_Apelido_49 = item.Doador;
                    rede.Posicao_Status_49 = (int)item.Status;
                }
                if (ii.Equals(50))
                {
                    rede.Posicao_ID_50 = item.ClienteId;
                    rede.Posicao_Apelido_50 = item.Doador;
                    rede.Posicao_Status_50 = (int)item.Status;
                }
                if (ii.Equals(51))
                {
                    rede.Posicao_ID_51 = item.ClienteId;
                    rede.Posicao_Apelido_51 = item.Doador;
                    rede.Posicao_Status_51 = (int)item.Status;
                }
                if (ii.Equals(52))
                {
                    rede.Posicao_ID_52 = item.ClienteId;
                    rede.Posicao_Apelido_52 = item.Doador;
                    rede.Posicao_Status_52 = (int)item.Status;
                }
                if (ii.Equals(53))
                {
                    rede.Posicao_ID_53 = item.ClienteId;
                    rede.Posicao_Apelido_53 = item.Doador;
                    rede.Posicao_Status_53 = (int)item.Status;
                }
                if (ii.Equals(54))
                {
                    rede.Posicao_ID_54 = item.ClienteId;
                    rede.Posicao_Apelido_54 = item.Doador;
                    rede.Posicao_Status_54 = (int)item.Status;
                }
                if (ii.Equals(55))
                {
                    rede.Posicao_ID_55 = item.ClienteId;
                    rede.Posicao_Apelido_55 = item.Doador;
                    rede.Posicao_Status_55 = (int)item.Status;
                }
                if (ii.Equals(56))
                {
                    rede.Posicao_ID_56 = item.ClienteId;
                    rede.Posicao_Apelido_56 = item.Doador;
                    rede.Posicao_Status_56 = (int)item.Status;
                }
                if (ii.Equals(57))
                {
                    rede.Posicao_ID_57 = item.ClienteId;
                    rede.Posicao_Apelido_57 = item.Doador;
                    rede.Posicao_Status_57 = (int)item.Status;
                }
                if (ii.Equals(58))
                {
                    rede.Posicao_ID_58 = item.ClienteId;
                    rede.Posicao_Apelido_58 = item.Doador;
                    rede.Posicao_Status_58 = (int)item.Status;
                }
                if (ii.Equals(59))
                {
                    rede.Posicao_ID_59 = item.ClienteId;
                    rede.Posicao_Apelido_59 = item.Doador;
                    rede.Posicao_Status_59 = (int)item.Status;
                }
                if (ii.Equals(60))
                {
                    rede.Posicao_ID_60 = item.ClienteId;
                    rede.Posicao_Apelido_60 = item.Doador;
                    rede.Posicao_Status_60 = (int)item.Status;
                }
                if (ii.Equals(61))
                {
                    rede.Posicao_ID_61 = item.ClienteId;
                    rede.Posicao_Apelido_61 = item.Doador;
                    rede.Posicao_Status_61 = (int)item.Status;
                }
                if (ii.Equals(62))
                {
                    rede.Posicao_ID_62 = item.ClienteId;
                    rede.Posicao_Apelido_62 = item.Doador;
                    rede.Posicao_Status_62 = (int)item.Status;
                }
                if (ii.Equals(63))
                {
                    rede.Posicao_ID_63 = item.ClienteId;
                    rede.Posicao_Apelido_63 = item.Doador;
                    rede.Posicao_Status_63 = (int)item.Status;
                }
                if (ii.Equals(64))
                {
                    rede.Posicao_ID_64 = item.ClienteId;
                    rede.Posicao_Apelido_64 = item.Doador;
                    rede.Posicao_Status_64 = (int)item.Status;
                }
                if (ii.Equals(65))
                {
                    rede.Posicao_ID_65 = item.ClienteId;
                    rede.Posicao_Apelido_65 = item.Doador;
                    rede.Posicao_Status_65 = (int)item.Status;
                }
                if (ii.Equals(66))
                {
                    rede.Posicao_ID_66 = item.ClienteId;
                    rede.Posicao_Apelido_66 = item.Doador;
                    rede.Posicao_Status_66 = (int)item.Status;
                }
                if (ii.Equals(67))
                {
                    rede.Posicao_ID_67 = item.ClienteId;
                    rede.Posicao_Apelido_67 = item.Doador;
                    rede.Posicao_Status_67 = (int)item.Status;
                }
                if (ii.Equals(68))
                {
                    rede.Posicao_ID_68 = item.ClienteId;
                    rede.Posicao_Apelido_68 = item.Doador;
                    rede.Posicao_Status_68 = (int)item.Status;
                }
                if (ii.Equals(69))
                {
                    rede.Posicao_ID_69 = item.ClienteId;
                    rede.Posicao_Apelido_69 = item.Doador;
                    rede.Posicao_Status_69 = (int)item.Status;
                }
                if (ii.Equals(70))
                {
                    rede.Posicao_ID_70 = item.ClienteId;
                    rede.Posicao_Apelido_70 = item.Doador;
                    rede.Posicao_Status_70 = (int)item.Status;
                }
                if (ii.Equals(71))
                {
                    rede.Posicao_ID_71 = item.ClienteId;
                    rede.Posicao_Apelido_71 = item.Doador;
                    rede.Posicao_Status_71 = (int)item.Status;
                }
                if (ii.Equals(72))
                {
                    rede.Posicao_ID_72 = item.ClienteId;
                    rede.Posicao_Apelido_72 = item.Doador;
                    rede.Posicao_Status_72 = (int)item.Status;
                }
                if (ii.Equals(73))
                {
                    rede.Posicao_ID_73 = item.ClienteId;
                    rede.Posicao_Apelido_73 = item.Doador;
                    rede.Posicao_Status_73 = (int)item.Status;
                }
                if (ii.Equals(74))
                {
                    rede.Posicao_ID_74 = item.ClienteId;
                    rede.Posicao_Apelido_74 = item.Doador;
                    rede.Posicao_Status_74 = (int)item.Status;
                }
                if (ii.Equals(75))
                {
                    rede.Posicao_ID_75 = item.ClienteId;
                    rede.Posicao_Apelido_75 = item.Doador;
                    rede.Posicao_Status_75 = (int)item.Status;
                }
                if (ii.Equals(76))
                {
                    rede.Posicao_ID_76 = item.ClienteId;
                    rede.Posicao_Apelido_76 = item.Doador;
                    rede.Posicao_Status_76 = (int)item.Status;
                }
                if (ii.Equals(77))
                {
                    rede.Posicao_ID_77 = item.ClienteId;
                    rede.Posicao_Apelido_77 = item.Doador;
                    rede.Posicao_Status_77 = (int)item.Status;
                }
                if (ii.Equals(78))
                {
                    rede.Posicao_ID_78 = item.ClienteId;
                    rede.Posicao_Apelido_78 = item.Doador;
                    rede.Posicao_Status_78 = (int)item.Status;
                }
                if (ii.Equals(79))
                {
                    rede.Posicao_ID_79 = item.ClienteId;
                    rede.Posicao_Apelido_79 = item.Doador;
                    rede.Posicao_Status_79 = (int)item.Status;
                }
                if (ii.Equals(80))
                {
                    rede.Posicao_ID_80 = item.ClienteId;
                    rede.Posicao_Apelido_80 = item.Doador;
                    rede.Posicao_Status_80 = (int)item.Status;
                }
                if (ii.Equals(81))
                {
                    rede.Posicao_ID_81 = item.ClienteId;
                    rede.Posicao_Apelido_81 = item.Doador;
                    rede.Posicao_Status_81 = (int)item.Status;
                }
                if (ii.Equals(82))
                {
                    rede.Posicao_ID_82 = item.ClienteId;
                    rede.Posicao_Apelido_82 = item.Doador;
                    rede.Posicao_Status_82 = (int)item.Status;
                }
                if (ii.Equals(83))
                {
                    rede.Posicao_ID_83 = item.ClienteId;
                    rede.Posicao_Apelido_83 = item.Doador;
                    rede.Posicao_Status_83 = (int)item.Status;
                }
                if (ii.Equals(84))
                {
                    rede.Posicao_ID_84 = item.ClienteId;
                    rede.Posicao_Apelido_84 = item.Doador;
                    rede.Posicao_Status_84 = (int)item.Status;
                }
                if (ii.Equals(85))
                {
                    rede.Posicao_ID_85 = item.ClienteId;
                    rede.Posicao_Apelido_85 = item.Doador;
                    rede.Posicao_Status_85 = (int)item.Status;
                }
                if (ii.Equals(86))
                {
                    rede.Posicao_ID_86 = item.ClienteId;
                    rede.Posicao_Apelido_86 = item.Doador;
                    rede.Posicao_Status_86 = (int)item.Status;
                }
                if (ii.Equals(87))
                {
                    rede.Posicao_ID_87 = item.ClienteId;
                    rede.Posicao_Apelido_87 = item.Doador;
                    rede.Posicao_Status_87 = (int)item.Status;
                }
                if (ii.Equals(88))
                {
                    rede.Posicao_ID_88 = item.ClienteId;
                    rede.Posicao_Apelido_88 = item.Doador;
                    rede.Posicao_Status_88 = (int)item.Status;
                }
                if (ii.Equals(89))
                {
                    rede.Posicao_ID_89 = item.ClienteId;
                    rede.Posicao_Apelido_89 = item.Doador;
                    rede.Posicao_Status_89 = (int)item.Status;
                }
                if (ii.Equals(90))
                {
                    rede.Posicao_ID_90 = item.ClienteId;
                    rede.Posicao_Apelido_90 = item.Doador;
                    rede.Posicao_Status_90 = (int)item.Status;
                }
                if (ii.Equals(91))
                {
                    rede.Posicao_ID_91 = item.ClienteId;
                    rede.Posicao_Apelido_91 = item.Doador;
                    rede.Posicao_Status_91 = (int)item.Status;
                }
                if (ii.Equals(92))
                {
                    rede.Posicao_ID_92 = item.ClienteId;
                    rede.Posicao_Apelido_92 = item.Doador;
                    rede.Posicao_Status_92 = (int)item.Status;
                }
                if (ii.Equals(93))
                {
                    rede.Posicao_ID_93 = item.ClienteId;
                    rede.Posicao_Apelido_93 = item.Doador;
                    rede.Posicao_Status_93 = (int)item.Status;
                }
                if (ii.Equals(94))
                {
                    rede.Posicao_ID_94 = item.ClienteId;
                    rede.Posicao_Apelido_94 = item.Doador;
                    rede.Posicao_Status_94 = (int)item.Status;
                }
                if (ii.Equals(95))
                {
                    rede.Posicao_ID_95 = item.ClienteId;
                    rede.Posicao_Apelido_95 = item.Doador;
                    rede.Posicao_Status_95 = (int)item.Status;
                }
                if (ii.Equals(96))
                {
                    rede.Posicao_ID_96 = item.ClienteId;
                    rede.Posicao_Apelido_96 = item.Doador;
                    rede.Posicao_Status_96 = (int)item.Status;
                }
                if (ii.Equals(97))
                {
                    rede.Posicao_ID_97 = item.ClienteId;
                    rede.Posicao_Apelido_97 = item.Doador;
                    rede.Posicao_Status_97 = (int)item.Status;
                }
                if (ii.Equals(98))
                {
                    rede.Posicao_ID_98 = item.ClienteId;
                    rede.Posicao_Apelido_98 = item.Doador;
                    rede.Posicao_Status_98 = (int)item.Status;
                }
                if (ii.Equals(99))
                {
                    rede.Posicao_ID_99 = item.ClienteId;
                    rede.Posicao_Apelido_99 = item.Doador;
                    rede.Posicao_Status_99 = (int)item.Status;
                }
                if (ii.Equals(100))
                {
                    rede.Posicao_ID_100 = item.ClienteId;
                    rede.Posicao_Apelido_100 = item.Doador;
                    rede.Posicao_Status_100 = (int)item.Status;
                }
                if (ii.Equals(101))
                {
                    rede.Posicao_ID_101 = item.ClienteId;
                    rede.Posicao_Apelido_101 = item.Doador;
                    rede.Posicao_Status_101 = (int)item.Status;
                }
                if (ii.Equals(102))
                {
                    rede.Posicao_ID_102 = item.ClienteId;
                    rede.Posicao_Apelido_102 = item.Doador;
                    rede.Posicao_Status_102 = (int)item.Status;
                }
                if (ii.Equals(103))
                {
                    rede.Posicao_ID_103 = item.ClienteId;
                    rede.Posicao_Apelido_103 = item.Doador;
                    rede.Posicao_Status_103 = (int)item.Status;
                }
                if (ii.Equals(104))
                {
                    rede.Posicao_ID_104 = item.ClienteId;
                    rede.Posicao_Apelido_104 = item.Doador;
                    rede.Posicao_Status_104 = (int)item.Status;
                }
                if (ii.Equals(105))
                {
                    rede.Posicao_ID_105 = item.ClienteId;
                    rede.Posicao_Apelido_105 = item.Doador;
                    rede.Posicao_Status_105 = (int)item.Status;
                }
                if (ii.Equals(106))
                {
                    rede.Posicao_ID_106 = item.ClienteId;
                    rede.Posicao_Apelido_106 = item.Doador;
                    rede.Posicao_Status_106 = (int)item.Status;
                }
                if (ii.Equals(107))
                {
                    rede.Posicao_ID_107 = item.ClienteId;
                    rede.Posicao_Apelido_107 = item.Doador;
                    rede.Posicao_Status_107 = (int)item.Status;
                }
                if (ii.Equals(108))
                {
                    rede.Posicao_ID_108 = item.ClienteId;
                    rede.Posicao_Apelido_108 = item.Doador;
                    rede.Posicao_Status_108 = (int)item.Status;
                }
                if (ii.Equals(109))
                {
                    rede.Posicao_ID_109 = item.ClienteId;
                    rede.Posicao_Apelido_109 = item.Doador;
                    rede.Posicao_Status_109 = (int)item.Status;
                }
                if (ii.Equals(110))
                {
                    rede.Posicao_ID_110 = item.ClienteId;
                    rede.Posicao_Apelido_110 = item.Doador;
                    rede.Posicao_Status_110 = (int)item.Status;
                }
                if (ii.Equals(111))
                {
                    rede.Posicao_ID_110 = item.ClienteId;
                    rede.Posicao_Apelido_111 = item.Doador;
                    rede.Posicao_Status_111 = (int)item.Status;
                }
                if (ii.Equals(112))
                {
                    rede.Posicao_ID_112 = item.ClienteId;
                    rede.Posicao_Apelido_112 = item.Doador;
                    rede.Posicao_Status_112 = (int)item.Status;
                }
                if (ii.Equals(113))
                {
                    rede.Posicao_ID_113 = item.ClienteId;
                    rede.Posicao_Apelido_113 = item.Doador;
                    rede.Posicao_Status_113 = (int)item.Status;
                }
                if (ii.Equals(114))
                {
                    rede.Posicao_ID_114 = item.ClienteId;
                    rede.Posicao_Apelido_114 = item.Doador;
                    rede.Posicao_Status_114 = (int)item.Status;
                }
                if (ii.Equals(115))
                {
                    rede.Posicao_ID_115 = item.ClienteId;
                    rede.Posicao_Apelido_115 = item.Doador;
                    rede.Posicao_Status_115 = (int)item.Status;
                }
                if (ii.Equals(116))
                {
                    rede.Posicao_ID_116 = item.ClienteId;
                    rede.Posicao_Apelido_116 = item.Doador;
                    rede.Posicao_Status_116 = (int)item.Status;
                }
                if (ii.Equals(117))
                {
                    rede.Posicao_ID_117 = item.ClienteId;
                    rede.Posicao_Apelido_117 = item.Doador;
                    rede.Posicao_Status_117 = (int)item.Status;
                }
                if (ii.Equals(118))
                {
                    rede.Posicao_ID_118 = item.ClienteId;
                    rede.Posicao_Apelido_118 = item.Doador;
                    rede.Posicao_Status_118 = (int)item.Status;
                }
                if (ii.Equals(119))
                {
                    rede.Posicao_ID_119 = item.ClienteId;
                    rede.Posicao_Apelido_119 = item.Doador;
                    rede.Posicao_Status_119 = (int)item.Status;
                }
                if (ii.Equals(120))
                {
                    rede.Posicao_ID_120 = item.ClienteId;
                    rede.Posicao_Apelido_120 = item.Doador;
                    rede.Posicao_Status_120 = (int)item.Status;
                }
                if (ii.Equals(121))
                {
                    rede.Posicao_ID_121 = item.ClienteId;
                    rede.Posicao_Apelido_121 = item.Doador;
                    rede.Posicao_Status_121 = (int)item.Status;
                }
                ii++;
            }
            return (rede);
        }

        public ActionResult Pendentes()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var cliente = SessionManager.UsuarioLogado.Login;

            return View(_doacao.GetAll().Where(x => x.Status.Equals(0)));
        }

    }
}