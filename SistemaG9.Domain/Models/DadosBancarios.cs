using SistemaG9.Domain.MetaData;
using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Domain.Models
{
    [MetadataType(typeof(DadosBancariosMetaData))]

    public class DadosBancarioViewModel
    {
        public int DadosId { get; set; }
        public int ClienteId { get; set; }
        public int BancoId { get; set; }
        public string Titular { get; set; }
        public int TipoConta { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string Operacao { get; set; }
        public virtual Bancos Banco { get; set; }
        public virtual Clientes Cliente { get; set; }
    }
}
