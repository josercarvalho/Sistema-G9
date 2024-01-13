using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Web.ViewModels
{
    public class CadastroViewModel
    {
        [Key]
        public int ClienteId { get; set; }

        public int PerfilUsuarioId { get; set; }

        [Required]
        [Display(Name = "Login de Acesso")]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Data Nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DataNascimento { get; set; }

        [Required]
        public string CPF { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [Display(Name ="Endereço")]
        public string Endereco { get; set; }

        [Required]
        [Display(Name ="Número")]
        public string Numero { get; set; }

        public string Complemento { get; set; }

        [Required]
        public string Bairro { get; set; }

        [Required]
        public string CEP { get; set; }

        [Display(Name = "País")]
        public int PaisId { get; set; }

        [Required]
        [Display(Name ="Cidade")]
        public int CidadeId { get; set; }

        [Required]
        [Display(Name ="Estado")]
        public int EstadoId { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Operadora { get; set; }

        public string WhatsApp { get; set; }

        public string SKYPE { get; set; }

        [ScaffoldColumn(false)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime DataCadastro { get; set; }

        //public int DadosId { get; set; }

        [Required]
        public int BancoId { get; set; }

        [Required]
        public int TipoConta { get; set; }

        [Required]
        [Display(Name ="Titular da Conta")]
        public string Titular { get; set; }


        [Required]
        [Display(Name = "Agência")]
        public string Agencia { get; set; }

        [Required]
        public string Conta { get; set; }

        [Display(Name = "Operação ou Variação")]
        public string Operacao { get; set; }
    }
}