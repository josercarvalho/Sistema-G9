using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaG9.Domain.Models
{
    public class Entrada
    {
        public int EntradaId { get; set; }
        public int Apelido { get; set; }
        public int Nivel { get; set; }
        public int Amigo { get; set; }
        public int Deposito { get; set; }
        public int Status { get; set; }

        public virtual Rede Rede { get; set; }
    }
}
