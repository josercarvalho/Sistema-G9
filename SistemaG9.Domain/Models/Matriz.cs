using System.Collections.Generic;

namespace SistemaG9.Domain.Models
{
    public class Matriz
    {
        public Matriz()
        {
            Doacoes = new List<Doacoes>();
            Nivels = new List<Nivel>();
        }

        public int MatrizId { get; set; }
        public string Nome { get; set; }
        public int Niveis { get; set; }
        public int Integrantes { get; set; }
        public virtual ICollection<Doacoes> Doacoes { get; set; }
        public virtual ICollection<Nivel> Nivels { get; set; }
    }
}
