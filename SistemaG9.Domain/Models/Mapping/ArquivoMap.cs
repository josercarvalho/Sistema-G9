using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SistemaG9.Domain.Models.Mapping
{
    public class ArquivoMap : EntityTypeConfiguration<Arquivos>
    {
        public ArquivoMap()
        {
            // Primary Key
            HasKey(t => t.ID);

            // Properties
            Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(150);

            Property(t => t.AnexoExtensao)
                .HasMaxLength(150);

            Property(t => t.AnexoTipo)
                .HasMaxLength(150);

            Property(t => t.Status)
                .HasMaxLength(150);

            // Table & Column Mappings
            ToTable("Arquivo");
            Property(t => t.ID).HasColumnName("ID");
            Property(t => t.ClienteId).HasColumnName("ClienteId");
            Property(t => t.Recebedor).HasColumnName("Recebedor");
            Property(t => t.Nome).HasColumnName("Nome");
            Property(t => t.AnexoBytes).HasColumnName("AnexoBytes");
            Property(t => t.AnexoExtensao).HasColumnName("AnexoExtensao");
            Property(t => t.AnexoTipo).HasColumnName("AnexoTipo");
            Property(t => t.DataEnvio).HasColumnName("DataEnvio");
            Property(t => t.DataCadastro).HasColumnName("DataCadastro");
            Property(t => t.Status).HasColumnName("Status");

            // Relationships
            HasRequired(t => t.Cliente)
                .WithMany(t => t.Arquivoes)
                .HasForeignKey(d => d.ClienteId);

        }
    }
}
