using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Domain.MetaData
{
    public class DadosBancariosMetaData
    {
        [Key]
        public int DadosId { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        [Display(Name = "Banco")]
        public int BancoId { get; set; }

        [Required]
        [Display(Name ="Titular da Conta")]
        public string Titular { get; set; }
        
        [Required]
        [Display(Name = "Tipo da Conta")]
        public int TipoConta { get; set; }

        [Required]
        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Required]
        [Display(Name ="Número da Conta")]
        public string Conta { get; set; }

        [Display(Name = "Operação ou Variação")]
        public string Operacao { get; set; }
    }
}
