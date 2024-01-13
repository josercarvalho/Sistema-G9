using System;

namespace SistemaG9.Domain.Models
{
    public class Doacoes
    {
        /// <summary>
        /// TIPO: 0 - ENTRADA, 1 - REENTRADA, 2 - DOAÇAO
        /// STATUS: 0 - ABERTO, 1 - ENVIADO , 2 - PENDENTE, 3 - CONFIRMADO
        /// </summary>
        public int DoacoesId { get; set; }
        public int MatrizId { get; set; }
        public int Nivel { get; set; }
        public int ClienteId { get; set; }
        public string Doador { get; set; }
        public string Recebedor { get; set; }
        public int Tipo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime ExpiraData { get; set; }
        public DateTime ConfirmaDoacao { get; set; }
        public string Comprovante { get; set; }
        public string AnexoExtensao { get; set; }
        public string AnexoTipo { get; set; }
        public DateTime DataEnvio { get; set; }
        public int Status { get; set; }

        public virtual Clientes Cliente { get; set; }
        public virtual DadosBancarioViewModel DadosBancarios { get; set; }
    }
}