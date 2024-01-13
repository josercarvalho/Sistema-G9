using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class ClientesController : Controller
    {
        //private readonly EntitieContext _db = new EntitieContext();
        private readonly IClienteService _cliente;
        private readonly IBancoService _banco;
        private readonly ICidadeService _cidade;
        private readonly IEstadoService _estado;
        private readonly IDadosBancariosService _dadosbancarios;
        private readonly IRedeService _rede;
        private readonly IDoacaoService _doacao;

        public ClientesController(IClienteService servCliente,
                                  IBancoService servBanco,
                                  ICidadeService servCidade,
                                  IEstadoService servEstado,
                                  IDadosBancariosService servDadosBanco,
                                  IRedeService servRede,
                                  IDoacaoService servDoacao)
        {
            _cliente = servCliente;
            _banco = servBanco;
            _cidade = servCidade;
            _estado = servEstado;
            _dadosbancarios = servDadosBanco;
            _rede = servRede;
            _doacao = servDoacao;
        }

        // GET: Escritorio/Clientes
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            return View(_cliente.GetAll().ToList());

        }

        // GET: Escritorio/Clientes/Create
        public ActionResult Edit()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var id = SessionManager.UsuarioLogado.ClienteId;

            Clientes Cliente = _cliente.GetById(id); // _db.Cliente.Find(id);
            if (Cliente == null)
            {
                return HttpNotFound();
            }

            //ViewBag.Estado = new SelectList(_estado.BuscaEstados().Select(x => new { x.EstadoId, x.Nome }).ToList(), "EstadoId", "Nome", Cliente.EstadoId);
            //ViewBag.Cidade = new SelectList( _cidade.BuscarPorEstado(Cliente.EstadoId).Select(x=> new { x.CidadeId, x.Nome }).ToList(), "CidadeId", "Nome", Cliente.CidadeId); // _db.Cidade.Where(d => d.EstadoId == Cliente.EstadoId).OrderBy(d => d.Nome).ToList();
            ViewBag.Estado = _estado.BuscaEstados().Select(x => new { x.EstadoId, x.Nome }).ToList();
            ViewBag.Cidade = _cidade.BuscarPorEstado(Cliente.EstadoId).Select(x => new { x.CidadeId, x.Nome }).ToList();

            return View(Cliente);
        }

        // POST: Escritorio/Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Clientes cliente)
        {

            //try
            //{
            //var login = SessionManager.UsuarioLogado.Login;
            //cliente.Login = login;

            //if (_cliente.IsExistEmail(cliente.Email))
            //    ModelState.AddModelError("Email", "E-mail cadastrado, tente outro...");

            var cpf = cliente.CPF;
            if (Functions.ValidaCpf(cpf) == false)
                ModelState.AddModelError("CPF", "CPF Inválido...");

            if (ModelState.IsValid)
            {
                cliente.Nome = cliente.Nome.ToUpper();
                _cliente.Update(cliente);
                //_db.Entry(cliente).State = EntityState.Modified;
                //_db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            //}
            //catch (Exception ex)
            //{
            //ModelState.AddModelError("", ex.Message);
            //}

            //ViewBag.Estado = new SelectList(_estado.BuscaEstados(), "EstadoId", "Nome", cliente.EstadoId);
            //ViewBag.Cidade = new SelectList(_cidade.BuscarPorEstado(cliente.EstadoId), "CidadeId", "Nome", cliente.CidadeId); // _db.Cidade.Where(d => d.EstadoId == Cliente.EstadoId).OrderBy(d => d.Nome).ToList();
            ViewBag.Estado = _estado.BuscaEstados().Select(x => new { x.EstadoId, x.Nome }).ToList();
            ViewBag.Cidade = _cidade.BuscarPorEstado(cliente.EstadoId).Select(x => new { x.CidadeId, x.Nome }).ToList();

            return View(cliente);
        }

        public ActionResult MinhaRede()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var login = SessionManager.UsuarioLogado.Login;
            var posicao = _rede.BuscarPorApelido(login).Posicao;

            var minhaRede = _rede.ListarMinhaRede(posicao); // _db.Rede.Include(x => x.Cliente).Where(x => x.Recebedor.Equals(id));

            return View(minhaRede);
        }

        public ActionResult Pendentes()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            //return View(_db.Doacao.Include(p => p.Cliente).Where(p => p.Status == 0).ToList());
            var cliente = SessionManager.UsuarioLogado.Login;

            return View(_doacao.ListarDoacoesPendentes(cliente));
        }

        public ActionResult MeusLogins()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var id = SessionManager.UsuarioLogado.ClienteId;
            var rede = _rede.ListarPorCliente(id);
            return View(rede);
        }

        private IEnumerable<Cidades> GetCidades(int estadoId)
        {
            return _cidade.BuscarPorEstado(estadoId); // _db.Cidade.Where(p => p.EstadoId == estadoId).ToList();
        }

        //[AcceptVerbs(HttpVerbs.Get)]
        public JsonResult LoadCidadeId(string estadoId)
        {
            var cidadeList = _cidade.BuscarPorEstado(Convert.ToInt32(estadoId)); // GetCidades(Convert.ToInt32(estadoId));
            var cidadeData = cidadeList.Select(m => new SelectListItem
            {
                Text = m.Nome,
                Value = m.CidadeId.ToString(CultureInfo.InvariantCulture),
            });
            return Json(cidadeData, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LiberaPagamento(string doador, string recebedor)
        {
            var mensagem = "Pagamento Liberado com Sucesso!";

            return Json(mensagem, JsonRequestBehavior.AllowGet);

        }
    }
}