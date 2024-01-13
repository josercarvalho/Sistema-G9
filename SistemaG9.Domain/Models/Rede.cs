namespace SistemaG9.Domain.Models
{
    public class Rede
    {
        public int RedeId { get; set; }
        public int MatrizId { get; set; }
        public int Nivel { get; set; }
        public int ClienteId { get; set; }
        public string Login { get; set; }
        public string Apelido { get; set; }
        public int RecebedorId { get; set; }
        public string ApelidoRecebedor { get; set; }
        public int Posicao { get; set; }
        public int Status { get; set; }
        public virtual Clientes Cliente { get; set; }
        public virtual Clientes Patrocinador { get; set; }
    }
}
