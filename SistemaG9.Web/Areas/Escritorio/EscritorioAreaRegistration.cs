using System.Web.Mvc;

namespace SistemaG9.Web.Areas.Escritorio
{
    public class EscritorioAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Escritorio";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Escritorio_default",
                "Escritorio/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "SistemaG9.Web.Areas.Escritorio.Controllers" }
            );
        }
    }
}