using System;

namespace SistemaG9.Domain.Models
{
    public class Arquivos
    {
        /// <summary>
        /// STATUS: 0 - PENDENTE, 1 - CONFIRMADO
        /// </summary>
        public int ID { get; set; }
        public int ClienteId { get; set; }
        public int Recebedor { get; set; }
        public string Nome { get; set; }
        public byte[] AnexoBytes { get; set; }
        public string AnexoExtensao { get; set; }
        public string AnexoTipo { get; set; }
        public DateTime DataEnvio { get; set; }
        public DateTime DataCadastro { get; set; }
        public string Status { get; set; }
        public virtual Clientes Cliente { get; set; }
    }
}
