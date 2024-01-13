using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaG9.Web.ViewModels
{
    public class PendentesViewModel
    {
        public int ClienteId { get; set; }
        public string Login { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string WhatsApp { get; set; }
        public string Email { get; set; }
    }
}