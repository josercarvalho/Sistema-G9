using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class DadosBancariosController : Controller
    {
        //private readonly EntitieContext _db = new EntitieContext();
        private readonly IDadosBancariosService _dadosBancarios;
        private readonly IDoacaoService _doacao;
        private readonly IBancoService _banco;
        private readonly IRedeService _rede;
        private readonly IClienteService _cliente;

        public DadosBancariosController(IDoacaoService servDoacao, 
                                        IBancoService servBanco, 
                                        IRedeService servRede,
                                        IDadosBancariosService servDadosBancarios,
                                        IClienteService servCliente)
        {
            _doacao = servDoacao;
            _banco = servBanco;
            _rede = servRede;
            _dadosBancarios = servDadosBancarios;
            _cliente = servCliente;
        }

        // GET: Escritorio/DadosBancarios
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var dadosBancarios = _dadosBancarios.GetAll().Where(x=>x.ClienteId.Equals(SessionManager.UsuarioLogado.ClienteId));  // _db.DadoBancarios.Include(d => d.Banco).Include(d => d.Cliente);
            return View(dadosBancarios);
        }

        // GET: Escritorio/DadosBancarios/Create
        public ActionResult Create()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            ViewBag.BancoId = new SelectList(_banco.GetAll(), "BancoId", "Nome");
            return View();
        }

        // POST: Escritorio/DadosBancarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DadosBancarioViewModel dadoBancarios)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dadoBancarios.ClienteId = SessionManager.UsuarioLogado.ClienteId;
                    _dadosBancarios.Add(dadoBancarios);

                    //var id = SessionManager.UsuarioLogado.ClienteId;
                    //var login = SessionManager.UsuarioLogado.Login;

                    ////Verifica se cliente já está na rede para adcionar e pegar a quem doar os 50 reais
                    //var retorno = _rede.ListarPorCliente(id).SingleOrDefault(); // _db.Rede.SingleOrDefault(x => x.ClienteId.Equals(id));
                    //if (retorno == null) _rede.CadastoNaRede(id, 1, login, login); // RegistrarEntrada(id, login);
                    //// NIVEL do cliente na matriz
                    ////var nivel = retorno.Nivel; // _db.Rede.SingleOrDefault(x => x.ClienteId.Equals(id)).Nivel;
                    ////RegistraDoacao(id, login, nivel);

                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }

            ViewBag.BancoId = new SelectList(_banco.GetAll(), "BancoId", "Nome", dadoBancarios.BancoId);
            return View(dadoBancarios);
        }

        public ActionResult Edit(int id)
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            //var id = SessionManager.UsuarioLogado.ClienteId;

            //id = SessionManager.UsuarioLogado.ClienteId;
            DadosBancarioViewModel dadosBancarios = _dadosBancarios.GetById(id); // _db.DadoBancarios.FirstOrDefault(p => p.ClienteId == id);
            if (dadosBancarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.BancoId = new SelectList(_banco.GetAll(), "BancoId", "Nome", dadosBancarios.BancoId);
            return View(dadosBancarios);
        }

        // POST: Escritorio/DadosBancarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DadosBancarioViewModel dadosBancarios)
        {
            if (ModelState.IsValid)
            {
                dadosBancarios.ClienteId = SessionManager.UsuarioLogado.ClienteId;
                dadosBancarios.Titular = dadosBancarios.Titular.ToUpper();
                _dadosBancarios.Update(dadosBancarios);
                //_db.Entry(dadosBancarios).State = EntityState.Modified;
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BancoId = new SelectList(_banco.GetAll(), "BancoId", "Nome", dadosBancarios.BancoId);
            return View(dadosBancarios);
        }

        [HttpPost]
        public JsonResult DeleteDadosBancarios(int id)
        {
            string mensagemErro = "Excluído com sucesso...";
            try
            {
                DadosBancarioViewModel dadosBancarios = _dadosBancarios.GetById(id);
                _dadosBancarios.Remove(dadosBancarios);
            }
            catch (Exception ex)
            {
                mensagemErro = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
            }
            return Json(mensagemErro, JsonRequestBehavior.DenyGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _banco.Dispose();
                _cliente.Dispose();
                _dadosBancarios.Dispose();
                _doacao.Dispose();
                _rede.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}