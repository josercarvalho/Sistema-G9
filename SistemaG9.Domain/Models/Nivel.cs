namespace SistemaG9.Domain.Models
{
    public class Nivel
    {
        public int NivelId { get; set; }
        public int MatrizId { get; set; }
        public int NivelMatriz { get; set; }
        public int Inicio { get; set; }
        public int Fim { get; set; }
        public int Entradas { get; set; }
        public decimal ValorEntrada { get; set; }
        public int Reentradas { get; set; }
        public decimal ValorReentrada { get; set; }
        public virtual Matriz Matriz { get; set; }
    }
}
