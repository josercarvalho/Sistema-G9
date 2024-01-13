using System.Web.Mvc;

namespace SistemaG9.Web.Controllers
{
    public class HomeController : Controller
    {
        #region Web Site
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ComoFunciona()
        {
            return View();
        }

        public ActionResult Faq()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Construcao()
        {
            ViewBag.Message = "Breve em funcionamento.";

            return View();
        }

        #endregion
    }
}