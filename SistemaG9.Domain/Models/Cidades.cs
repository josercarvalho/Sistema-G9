using System.Collections.Generic;

namespace SistemaG9.Domain.Models
{
    public class Cidades
    {
        public int CidadeId { get; set; }
        public int EstadoId { get; set; }
        public string Nome { get; set; }
        public virtual Estados Estado { get; set; }
    }
}
