using System.Collections.Generic;

namespace SistemaG9.Domain.Models
{
    public class Pais
    {
        public Pais()
        {
            Estadoes = new List<Estados>();
        }

        public int PaisId { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Estados> Estadoes { get; set; }
    }
}
