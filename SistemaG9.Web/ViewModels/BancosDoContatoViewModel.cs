using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SistemaG9.Web.ViewModels
{
    public class BancosDoContatoViewModel
    {
        public int ID { get; set; }
        public int ClienteId { get; set; }
        public string Login { get; set; }
        public string Banco { get; set; }
        public int BancoId { get; set; }

    }
}