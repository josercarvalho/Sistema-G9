using System;
using System.Collections.Generic;

namespace SistemaG9.Domain.Models
{
    public class PerfilUsuario
    {
        public PerfilUsuario()
        {
            Clientes = new List<Clientes>();
        }

        public int PerfilUsuarioId { get; set; }
        public string NomPerfil { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool FlAdminMaster { get; set; }
        public bool FlAtivo { get; set; }
        public virtual ICollection<Clientes> Clientes { get; set; }
    }
}
