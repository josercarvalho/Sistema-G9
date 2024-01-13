using SistemaG9.Domain.Models;
using System.Collections.Generic;

namespace SistemaG9.Web.ViewModels
{
    public class DadoBancarioViewModel
    {
        public int ID { get; set; }
        public int Nivel { get; set; }
        public int ClienteId { get; set; }
        public int Tipo { get; set; }
        public string Nome { get; set; }
        public string Doador { get; set; }
        public string Recebedor { get; set; }
        public string Titular { get; set; }
        public string Telefone { get; set; }
        public string WhatsApp { get; set; }
        public string Banco { get; set; }
        public int TipoConta { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Operacao { get; set; }
        public decimal Valor { get; set; }
        public string Comprovante { get; set; }

    }
}