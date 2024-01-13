namespace SistemaG9.Domain.Models
{
    public class UpLine
    {
        public int UpLineId { get; set; }
        public int ClienteId { get; set; }
        public int OrigemId { get; set; }
        public string Login { get; set; }
        public int Nivel { get; set; }
        public decimal Valor { get; set; }
        public int Status { get; set; }
        public virtual Clientes Cliente { get; set; }
    }
}
