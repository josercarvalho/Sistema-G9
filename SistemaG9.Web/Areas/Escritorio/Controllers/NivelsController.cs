using SistemaG9.Domain.Interfaces.Services;
using SistemaG9.Domain.Models;
using SistemaG9.Web.Util;
using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio.Controllers
{
    public class NivelsController : Controller
    {
        private readonly INivelService _servNivel;
        private readonly IMatrizService _servMatriz;

        public NivelsController(INivelService servNivel, IMatrizService servMatriz)
        {
            _servNivel = servNivel;
            _servMatriz = servMatriz;
        }

        // GET: Escritorio/Nivels
        public ActionResult Index()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            var niveis = _servNivel.GetAll(); // _db.Niveis.Include(n => n.Matriz);
            return View(niveis);
        }

        // GET: Escritorio/Nivels/Create
        public ActionResult Create()
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            ViewBag.MatrizId = new SelectList(_servMatriz.GetAll(), "MatrizId", "Nome");
            return View();
        }

        // POST: Escritorio/Nivels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Nivel nivel)
        {
            if (ModelState.IsValid)
            {
                _servNivel.Add(nivel);
                ModelState.Clear();
                return View();
            }

            ViewBag.MatrizId = new SelectList(_servMatriz.GetAll(), "MatrizId", "Nome", nivel.MatrizId);
            return View(nivel);
        }

        // GET: Escritorio/Nivels/Edit/5
        public ActionResult Edit(int id)
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Nivel nivel = _servNivel.GetById(id); // _db.Niveis.Find(id);
            if (nivel == null)
            {
                return HttpNotFound();
            }
            ViewBag.MatrizId = new SelectList(_servMatriz.GetAll(), "MatrizId", "Nome", nivel.MatrizId);
            return View(nivel);
        }

        // POST: Escritorio/Nivels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Nivel nivel)
        {
            if (ModelState.IsValid)
            {
                _servNivel.Update(nivel);
                //_db.Entry(nivel).State = EntityState.Modified;
                //_db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MatrizId = new SelectList(_servMatriz.GetAll(), "MatrizId", "Nome", nivel.MatrizId);
            return View(nivel);
        }

        // GET: Escritorio/Nivels/Delete/5
        public ActionResult Delete(int id)
        {
            if (SessionManager.IsAuthenticated == false) return RedirectToAction("Login", "Account");

            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            Nivel nivel = _servNivel.GetById(id); // _db.Niveis.Find(id);
            if (nivel == null)
            {
                return HttpNotFound();
            }
            return View(nivel);
        }

        // POST: Escritorio/Nivels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nivel nivel = _servNivel.GetById(id); // _db.Niveis.Find(id);
            _servNivel.Remove(nivel);
            //_db.Niveis.Remove(nivel);
            //_db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _servNivel.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}