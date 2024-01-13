using System.Collections.Generic;

namespace SistemaG9.Domain.Models
{
    public class Bancos
    {
        public Bancos()
        {
            DadosBancarios = new List<DadosBancarioViewModel>();
        }

        public int Id { get; set; }
        public int BancoId { get; set; }
        public string Nome { get; set; }
        public virtual ICollection<DadosBancarioViewModel> DadosBancarios { get; set; }
    }
}
