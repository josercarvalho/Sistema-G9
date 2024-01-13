using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Domain.MetaData
{
    public class ClientesMetaData
    {
        public int ClienteId { get; set; }
        [Required]
        [Display(Name = "Perfil Usuário")]
        public int PerfilUsuarioId { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Numero { get; set; }
        public string Complemento { get; set; }
        [Required]
        public string Bairro { get; set; }
        [Required]
        public string CEP { get; set; }
        public int PaisId { get; set; }
        [Required]
        public int CidadeId { get; set; }
        [Required]
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
        public int? Status { get; set; }
    }
}
