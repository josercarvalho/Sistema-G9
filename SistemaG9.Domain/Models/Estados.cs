using System.Collections.Generic;

namespace SistemaG9.Domain.Models
{
    public class Estados
    {
        public Estados()
        {
            Cidades = new List<Cidades>();
        }

        public int EstadoId { get; set; }
        public int PaisId { get; set; }
        public string Sigla { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<Cidades> Cidades { get; set; }
        public virtual Pais Pai { get; set; }
    }
}
