using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class PerfilUsuarioMap : EntityTypeConfiguration<PerfilUsuario>
    {
        public PerfilUsuarioMap()
        {
            // Primary Key
            HasKey(t => t.PerfilUsuarioId);

            // Properties
            Property(t => t.NomPerfil)
                .IsRequired()
                .HasMaxLength(200);

            // Table & Column Mappings
            ToTable("PerfilUsuarios");
            Property(t => t.PerfilUsuarioId).HasColumnName("PerfilUsuarioId");
            Property(t => t.NomPerfil).HasColumnName("NomPerfil");
            Property(t => t.DataCadastro).HasColumnName("DataCadastro");
            Property(t => t.FlAdminMaster).HasColumnName("FlAdminMaster");
            Property(t => t.FlAtivo).HasColumnName("FlAtivo");
        }
    }
}
