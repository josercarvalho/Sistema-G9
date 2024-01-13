using SistemaG9.Domain.Interfaces.Repositories;
using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class MatrizsController : Controller
    {
        private readonly IMatrizService _servMatriz;
        private readonly IClienteService _servCliente;
        private readonly Domain.Interfaces.Repositories.IRedeService _repoRede;

        public MatrizsController(IMatrizService servMatriz, IClienteService servCliente, Domain.Interfaces.Repositories.IRedeService repoRede)
        {
            _servMatriz = servMatriz;
            _servCliente = servCliente;
            _repoRede = repoRede;
        }

        // GET: Escritorio/Matrizs
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            return View(_servMatriz.GetAll());
        }

        // GET: Escritorio/Matrizs/Create
        public ActionResult Create()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            return View();
        }

        // POST: Escritorio/Matrizs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Matriz matriz)
        {
            if (ModelState.IsValid)
            {
                _servMatriz.Add(matriz);

                //Cadastrar topo da Matriz
                var sistema1 = _servCliente.ListarPorLogin("sistema1"); // _db.Cliente.FirstOrDefault(x => x.Login == "sistema1");
                var nivel1 = new Rede();
                nivel1.MatrizId = matriz.MatrizId;
                nivel1.Nivel = 0;
                nivel1.ClienteId = sistema1.ClienteId;
                nivel1.Login = sistema1.Login;
                nivel1.Apelido = sistema1.Login;
                nivel1.RecebedorId = sistema1.ClienteId;
                nivel1.ApelidoRecebedor = "sistema1";
                nivel1.Posicao = 1;
                nivel1.Status = 1;
                _repoRede.Add(nivel1);

                var sistema2 = _servCliente.ListarPorLogin("sistema2"); // _db.Cliente.FirstOrDefault(x => x.Login == "sistema2");
                var nivel2 = new Rede();
                nivel2.MatrizId = matriz.MatrizId;
                nivel2.Nivel = 0;
                nivel2.ClienteId = sistema2.ClienteId;
                nivel2.Login = sistema2.Login;
                nivel2.Apelido = sistema2.Login;
                nivel2.RecebedorId = sistema2.ClienteId;
                nivel2.ApelidoRecebedor = "sistema1";
                nivel2.Posicao = 1;
                nivel2.Status = 1;
                _repoRede.Add(nivel2);

                var sistema3 = _servCliente.ListarPorLogin("sistema3"); // _db.Cliente.FirstOrDefault(x => x.Login == "sistema3");
                var nivel3 = new Rede();
                nivel3.MatrizId = matriz.MatrizId;
                nivel3.Nivel = 0;
                nivel3.ClienteId = sistema3.ClienteId;
                nivel3.Login = sistema3.Login;
                nivel3.Apelido = sistema3.Login;
                nivel3.RecebedorId = sistema3.ClienteId;
                nivel3.ApelidoRecebedor = "sistema2";
                nivel3.Posicao = 1;
                nivel3.Status = 1;
                _repoRede.Add(nivel3);

                var sistema4 = _servCliente.ListarPorLogin("sistema4"); // _db.Cliente.FirstOrDefault(x => x.Login == "sistema4");
                var nivel4 = new Rede();
                nivel4.MatrizId = matriz.MatrizId;
                nivel4.Nivel = 0;
                nivel4.ClienteId = sistema4.ClienteId;
                nivel4.Login = sistema4.Login;
                nivel4.Apelido = sistema4.Login;
                nivel4.RecebedorId = sistema4.ClienteId;
                nivel4.ApelidoRecebedor = "sistema3";
                nivel4.Posicao = 1;
                nivel4.Status = 1;
                _repoRede.Add(nivel4);
                
                return RedirectToAction("Index");
            }

            return View(matriz);
        }

        // GET: Escritorio/Matrizs/Edit/5
        public ActionResult Edit(int id)
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Matriz matriz = _servMatriz.GetById(id); // _db.Matriz.Find(id);
            if (matriz == null)
            {
                return HttpNotFound();
            }
            return View(matriz);
        }

        // POST: Escritorio/Matrizs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( Matriz matriz)
        {
            if (ModelState.IsValid)
            {
                _servMatriz.Update(matriz);
                //_db.Entry(matriz).State = EntityState.Modified;
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(matriz);
        }

        // GET: Escritorio/Matrizs/Delete/5
        public ActionResult Delete(int id)
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Matriz matriz = _servMatriz.GetById(id); // _db.Matriz.Find(id);
            if (matriz == null)
            {
                return HttpNotFound();
            }
            return View(matriz);
        }

        // POST: Escritorio/Matrizs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Matriz matriz = _servMatriz.GetById(id); //_db.Matriz.Find(id);
            _servMatriz.Remove(matriz);
            //_db.Matriz.Remove(matriz);
            //_db.SaveChanges();
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _servMatriz.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}