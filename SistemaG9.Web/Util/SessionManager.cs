using System.Web;
using SistemaG9.Domain.Models;

namespace SistemaG9.Web.Util
{
    public class SessionManager
    {
        public static Clientes UsuarioLogado
        {
            set
            {

                HttpContext.Current.Session.Add("UsuarioLogado", value);
            }
            get
            {
                return (Clientes)HttpContext.Current.Session["UsuarioLogado"];
            }

        }

        public static Rede UsuarioNaRede
        {
            set
            {

                HttpContext.Current.Session.Add("UsuarioNaRede", value);
            }
            get
            {
                return (Rede)HttpContext.Current.Session["UsuarioNaRede"];
            }
        }
        
        public static Doacoes UsuarioDoacoes
        {
            set
            {

                HttpContext.Current.Session.Add("UsuarioDoacoes", value);
            }
            get
            {
                return (Doacoes)HttpContext.Current.Session["UsuarioDoacoes"];
            }
        }

        public static bool IsAuthenticated
        {
            get
            {
                return ((Clientes)HttpContext.Current.Session["UsuarioLogado"]) != null;
            }
        }
    }
}
