using SistemaG9.Domain.MetaData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaG9.Domain.Models
{
    [MetadataType(typeof(ClientesMetaData))]
    public class Clientes
    {
        public Clientes()
        {
            Arquivoes = new List<Arquivos>();
            DadosBancarios = new List<DadosBancarioViewModel>();
            Doacoes = new List<Doacoes>();
            Redes = new List<Rede>();
            Redes1 = new List<Rede>();
            UpLines = new List<UpLine>();
        }

        public int ClienteId { get; set; }
        public int PerfilUsuarioId { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string CEP { get; set; }
        public int PaisId { get; set; }
        public int CidadeId { get; set; }
        public int EstadoId { get; set; }
        public string Telefone { get; set; }
        public string Operadora { get; set; }
        public string WhatsApp { get; set; }
        public string SKYPE { get; set; }
        public DateTime DataCadastro { get; set; }
        //public int Reentradas { get; set; }
        public int? Status { get; set; }
        public virtual ICollection<Arquivos> Arquivoes { get; set; }
        public virtual PerfilUsuario PerfilUsuario { get; set; }
        public virtual Nivel Nivel { get; set; }
        public virtual ICollection<DadosBancarioViewModel> DadosBancarios { get; set; }
        public virtual ICollection<Doacoes> Doacoes { get; set; }
        public virtual ICollection<Rede> Redes { get; set; }
        public virtual ICollection<Rede> Redes1 { get; set; }
        public virtual ICollection<UpLine> UpLines { get; set; }

    }
}
