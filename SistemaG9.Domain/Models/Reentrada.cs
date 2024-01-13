namespace SistemaG9.Domain.Models
{
    public partial class Reentrada
    {
        public int ReentradaId { get; set; }
        public int Apelido { get; set; }
        public int Nivel { get; set; }
        public int Amigo { get; set; }
        public int Deposito { get; set; }
        public int Status { get; set; }

        public virtual Rede Rede { get; set; }
    }
}
